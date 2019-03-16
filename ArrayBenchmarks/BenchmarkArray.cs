using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Svelto.DataStructures
{
    /// <summary>
    /// The objective of this benchmark is just proving that using managed arrays is good enough!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe class UnmanagedMemoryManager<T> : MemoryManager<T>
    {
        readonly int    _elementCount;
        readonly IntPtr _ptr;

        public UnmanagedMemoryManager(int elementCount)
        {
            _elementCount = elementCount;

            _ptr = Marshal.AllocHGlobal(elementCount * Unsafe.SizeOf<T>());
        }

        protected override void Dispose(bool disposing)
        {
            Marshal.FreeHGlobal(_ptr);
        }

        public override Span<T> GetSpan()
        {
            return new Span<T>((void*) _ptr, _elementCount);
        }

        public override MemoryHandle Pin(int elementIndex)
        {
            throw new Exception();
        }

        public override void Unpin()
        {
            throw new Exception();
        }
    }

    //[DisassemblyDiagnoser(printAsm: true, printSource: true)] // !!! use the new diagnoser!!
    //[ClrJob, MonoJob, CoreJob]
    // [EtwProfiler]
    //   [HardwareCounters(HardwareCounter.BranchMispredictions,HardwareCounter.BranchInstructions)]
    //CoreRt --coreRtVersion "1.0.0-alpha-27515-01"
    //[MaxWarmupCount(2)]
    //[MaxIterationCount(2)]
    //[MinWarmupCount(1)]
    //[MinIterationCount(1)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public unsafe class ArrayBenchmark
    {
        int*                        unmanagedArray;
        Memory<int>                 unmanagedMemory;
        int*                        arrayPinned;
        GCHandle                    arrayPinnedGC;
        int[]                       managedArray;
        List<int>                   list;
        UnmanagedMemoryManager<int> unmanagedMemoryManager;
        Memory<int>                 managedMemory;
        const int                   TotalSize = 10_000_000;
        const int                   SizeSmallForRandomAccess = 1_000_000;
        int[]                       RandomAccess = new int[TotalSize];

        [GlobalSetup]
        public void GlobalSetupA()
        {
            unmanagedArray = (int*) Marshal.AllocHGlobal(TotalSize * sizeof(int));
            arrayPinnedGC = GCHandle.Alloc(new int[TotalSize], GCHandleType.Pinned);
            arrayPinned = (int*) arrayPinnedGC.AddrOfPinnedObject();
            unmanagedMemoryManager = new UnmanagedMemoryManager<int>(TotalSize);
            unmanagedMemory = unmanagedMemoryManager.Memory;
            managedMemory = new Memory<int>(new int[TotalSize]);
            managedArray = new int[TotalSize];
            list = new List<int>(TotalSize);
            var random = new Random();
            for (int i = 0; i < list.Capacity; i++)
            {
                list.Add(i);
                RandomAccess[i] = random.Next() % TotalSize;
            }
        }

        [GlobalCleanup]
        public void GlobalCleanUpA()
        {
            Marshal.FreeHGlobal(new IntPtr(unmanagedArray));
            arrayPinnedGC.Free();
            ((IDisposable) unmanagedMemoryManager).Dispose();
        }

        [Benchmark(Baseline = true), BenchmarkCategory("StandardArrayInsert")]
        public void StandardArrayInsert()
        {
            for (int i = 0; i < managedArray.Length; i++) managedArray[i] = i;
        }

        [Benchmark, BenchmarkCategory("StandardUnmanagedArrayInsert")]
        public void StandardUnmanagedArrayInsert()
        {
            for (int i = 0; i < TotalSize; i++) unmanagedArray[i] = i;
        }

        [Benchmark, BenchmarkCategory("StandardPinnedArrayInsert")]
        public void StandardPinnedArrayInsert()
        {
            for (int i = 0; i < TotalSize; i++) arrayPinned[i] = i;
        }

        [Benchmark, BenchmarkCategory("StandardListInsert")]
        public void StandardListInsert()
        {
            for (int i = 0; i < list.Count; i++) list[i] = i;
        }

        [Benchmark, BenchmarkCategory("StandardManagedSpanInsert")]
        public void StandardManagedSpanInsert()
        {
            var sarray = managedMemory.Span;
            for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
        }

        [Benchmark, BenchmarkCategory("StandardUnmanagedSpanInsert")]
        public void StandardUnmanagedSpanInsert()
        {
            var sarray = unmanagedMemory.Span;
            for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
        }

        [Benchmark, BenchmarkCategory("StandardUnmanagedSpanBenInsert")]
        public void StandardUnmanagedSpanBenInsert()
        {
            var sarray = unmanagedMemory.Span;
            ref var firstElement = ref MemoryMarshal.GetReference(sarray);
            for (int i = 0; i < sarray.Length; i++) Unsafe.Add(ref firstElement, i) = i;
        }
        
        [Benchmark, BenchmarkCategory("StandardArrayInsertChecked")]
        public void StandardArrayInsertChecked()
        {
            for (int i = 0; i < SizeSmallForRandomAccess; i++) 
                   managedArray[RandomAccess[i]] = i;
        }

        [Benchmark, BenchmarkCategory("StandardUnmanagedArrayInsertChecked")]
        public void StandardUnmanagedArrayInsertChecked()
        {
            for (int i = 0; i < SizeSmallForRandomAccess; i++) unmanagedArray[RandomAccess[i] ] = i;
        }

        [Benchmark, BenchmarkCategory("StandardPinnedArrayInsertChecked")]
        public void StandardPinnedArrayInsertChecked()
        {
            for (int i = 0; i < SizeSmallForRandomAccess; i++) arrayPinned[RandomAccess[i] ] = i;
        }

        [Benchmark, BenchmarkCategory("StandardListInsertChecked")]
        public void StandardListInsertChecked()
        {
            for (int i = 0; i < SizeSmallForRandomAccess; i++) list[RandomAccess[i] ] = i;
        }

        [Benchmark, BenchmarkCategory("StandardManagedSpanInsertChecked")]
        public void StandardManagedSpanInsertChecked()
        {
            var sarray = managedMemory.Span;
            for (int i = 0; i < SizeSmallForRandomAccess; i++) sarray[RandomAccess[i]] = i;
        }

        [Benchmark, BenchmarkCategory("StandardUnmanagedSpanInsertChecked")]
        public void StandardUnmanagedSpanInsertChecked()
        {
            var sarray = unmanagedMemory.Span;
            for (int i = 0; i < SizeSmallForRandomAccess; i++) sarray[RandomAccess[i] ] = i;
        }

        [Benchmark, BenchmarkCategory("StandardUnmanagedSpanBenInsertChecked")]
        public void StandardUnmanagedSpanBenInsertChecked()
        {
            var sarray = unmanagedMemory.Span;
            ref var firstElement = ref MemoryMarshal.GetReference(sarray);
            for (int i = 0; i < SizeSmallForRandomAccess; i++) Unsafe.Add(ref firstElement, RandomAccess[i] ) = i;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<ArrayBenchmark>();
             BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);
        }
    }
}

