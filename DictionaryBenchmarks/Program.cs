#if later
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Svelto.DataStructures.Experimental;

namespace Svelto.DataStructures
{
    public struct Test
    {
        public int v;
        public int i;

        public Test(int i) : this()
        {
            this.i = i;
        }
    }
   

    //[DisassemblyDiagnoser(printAsm: true, printSource: true)] // !!! use the new diagnoser!!
    //[RyuJitX64Job, ClrJob, MonoJob, CoreJob(baseline: true), CoreRtJob]
    [MonoJob()]
    // [EtwProfiler]
    //   [HardwareCounters(HardwareCounter.BranchMispredictions,HardwareCounter.BranchInstructions)]
    [MaxWarmupCount(2)]
    [MaxIterationCount(2)]
    [MinWarmupCount(1)]
    [MinIterationCount(1)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public unsafe class DictionaryBenchmark
    {
        int                         dictionarysize;
        FasterDictionary<uint, Test> fasterDictionary;
        NewFasterDictionary2<uint, Test> fasterDictionary2;
        Dictionary<uint, Test>       dictionary;
        uint[]                       numbers;
        Test[] tests;
        int* array;
        Memory<int> memory;
        int* arrayPinned;
        GCHandle arrayPinnedGC;
        int[] managedArray;

        public DictionaryBenchmark()
        {
            dictionarysize = 10000000;

            numbers = new uint[dictionarysize];
            tests = new Test[dictionarysize];
            var r = new Random();
            for (int i = 0; i < dictionarysize; i++) numbers[i] = (uint) r.Next();
//            for (int i = 0; i < dictionarysize; i++) numbers[i] = (uint) i;
            for (int i = 0; i < dictionarysize; i++) tests[i] = new Test(i);

            fasterDictionary = new FasterDictionary<uint, Test>(dictionarysize);
            fasterDictionary2 = new NewFasterDictionary2<uint, Test>(dictionarysize);
            dictionary = new Dictionary<uint, Test>(dictionarysize);

            array = (int*) Marshal.AllocHGlobal(dictionarysize * 4);
            memory = new SafeHandleMemoryManager<int>(dictionarysize).Memory;
        }

    //    [GlobalSetup(Targets = new[] {nameof(NewGet2), nameof(NewGet), nameof(StandardGet)})]
        public void GlobalSetupB()
        {
            for (int i = 0; i < dictionarysize; i++) dictionary[numbers[i]] = new Test(i);
            for (int i = 0; i < dictionarysize; i++) fasterDictionary[numbers[i]] = new Test(i);
            for (int i = 0; i < dictionarysize; i++) fasterDictionary2[numbers[i]] = new Test(i);
        }
        
        [GlobalSetup(Targets = new[]
        {
            nameof(StandardArrayInsert), nameof(StandardUnmanagedArrayInsert), nameof(StandardListInsert),
            nameof(StandardManagedSpanInsert), nameof(StandardUnmanagedSpanInsert), nameof(StandardUnmanagedSpanBenInsert),
        })]
        public void GlobalSetupA()
        {
            array = (int*) Marshal.AllocHGlobal(dictionarysize * sizeof(int));
            arrayPinnedGC = GCHandle.Alloc(new int[dictionarysize], GCHandleType.Pinned);
            arrayPinned =  (int*)arrayPinnedGC.AddrOfPinnedObject();
            memory = new SafeHandleMemoryManager<int>(dictionarysize).Memory;
            managedArray = new int[dictionarysize];
        }

        [GlobalCleanup(Targets = new[]
        {
            nameof(StandardArrayInsert), nameof(StandardUnmanagedArrayInsert), nameof(StandardListInsert),
            nameof(StandardManagedSpanInsert), nameof(StandardUnmanagedSpanInsert),
            nameof(StandardUnmanagedSpanBenInsert),
        })]

        public void GlobalCleanUpA()
        {
            Marshal.Release(new IntPtr(array));
            arrayPinnedGC.Free();
        }
        
        
        [BenchmarkCategory("Managed array insert"), Benchmark(Baseline = true)]
        public void StandardArrayInsert()
        {
            for (uint i = 0; i < managedArray.Length; i++) managedArray[i] = i;
        }
        
        [BenchmarkCategory("unmanaged array insert")]
        public void StandardUnmanagedArrayInsert()
        {
            for (uint i = 0; i < numbers.Length; i++) numbers[i] = i;
        }
        
        [BenchmarkCategory("pinned array insert")]
        public void StandardPinnedArrayInsert()
        {
            for (uint i = 0; i < numbers.Length; i++) numbers[i] = i;
        }
        
        [BenchmarkCategory("list insert")]
        public void StandardListInsert()
        {
            var sarray = memory.Span;
            for (uint i = 0; i < numbers.Length; i++) numbers[i] = i;
        }
        
        [BenchmarkCategory("managed span insert")]
        public void StandardManagedSpanInsert()
        {
            var sarray = memory.Span;
            for (uint i = 0; i < numbers.Length; i++) numbers[i] = i;
        }
        
        [BenchmarkCategory("unmanaged span insert")]
        public void StandardUnmanagedSpanInsert()
        {
            var sarray = memory.Span;
            for (uint i = 0; i < numbers.Length; i++) numbers[i] = i;
        }
        
        [BenchmarkCategory("unmanaged span Ben's idea insert")]
        public void StandardUnmanagedSpanBenInsert()
        {
            var sarray = memory.Span;
            for (uint i = 0; i < numbers.Length; i++) numbers[i] = i;
        }

      //  [BenchmarkCategory("Insert"), Benchmark(Baseline = true)]
        public void StandardInsert()
        {
            var sarray = memory.Span;
            for (uint i = 0; i < numbers.Length; i++) numbers[i] = i;
        }

      //  [BenchmarkCategory("Insert"), Benchmark]
        public void NewInsert()
        {
            var sarray = memory.Span;
            for (int i = 0; i < dictionarysize; i++) array[i] = i;
        }
        
    //   [BenchmarkCategory("Insert"), Benchmark]
        public void NewInsert2()
        {
            //for (int i = 0; i < dictionarysize; i++)
              //  reduce2(133, 3);
            //for (int i = 0; i < dictionarysize; i++) fasterDictionary2[numbers[i]] = new Test(i);
            var sarray = memory.Span;
            for (int i = 0; i < sarray.Length; i++) sarray[i] = i;
        }
        
       // [BenchmarkCategory("Get"), Benchmark(Baseline = true)]
        public void StandardGet()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
                X = dictionary[numbers[i]];
            }
        }

      //  [BenchmarkCategory("Get"), Benchmark]
        public void NewGet()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
                X = fasterDictionary[numbers[i]];
            }
        }
        
    //    [BenchmarkCategory("Get"), Benchmark]
        public void NewGet2()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
                X = fasterDictionary2[numbers[i]];
            }
        }
        
//        [BenchmarkCategory("Get"), Benchmark]
        public void NewGet3()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
                X = tests[numbers[i]];
            }
        }

        public void CheckCollisions()
        {
//            for (int i = 0; i < dictionarysize; i++) fasterDictionary[numbers[i]] = new Test(i);
            //for (int i = 0; i < dictionarysize; i++) fasterDictionary2[numbers[i]] = new Test(i);
            
            //Console.Log(fasterDictionary.Collisions.ToString());
            //Console.Log(fasterDictionary2.Collisions.ToString());

            var array = (int *)Marshal.AllocHGlobal(dictionarysize * 4);
            
            for (int i = 0; i < dictionarysize; i++) array[i] = i;
        }
        
        static uint reduce(uint x, int N) {
            
            return (uint) (x % N);
        }
        
        static uint reduce2(uint x, int N) 
        {
            unchecked
            {
                ulong hash = x;
                hash ^= hash >> 32;
                hash = (11400714819323198485 * hash) >> 32;
                return (uint) ((long) hash * N >> 32) ;
            }
        }
        
        public static void Test2()
        {
            Random rand = new Random();
            
            FasterDictionary<int, int>[] dic = new FasterDictionary<int, int>[10];

            for (int i = 0; i < 10; i++)
            {
                dic[i] = new FasterDictionary<int, int>();
            }

            long iterations = 0;
            
            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    var next = rand.Next();
                    if (next % 3 == 0)
                    {
                        var key = next % 30000;
                        dic[i][key] = next;
                        if (dic[i].ContainsKey(key) == false)
                            throw new Exception("asd");
                    }
                }
                
                for (int i = 0; i < 10; i++)
                {
                    var next = rand.Next();
                    if (next % 3 == 0)
                    {
                        var key = next % 30000;
                        dic[i].Remove(key);
                        if (dic[i].ContainsKey(key) == true)
                            throw new Exception("asd");
                    }
                }

                iterations++;
                
                Console.Log(iterations.ToString());
            }
        }
        
          public static void Profiling()
        {
            int dictionarysize = 10000000;

            int[] numbers = new int[dictionarysize];
            var r = new Random();
            for (int i = 0; i < dictionarysize; i++) numbers[i] = r.Next();

            FasterDictionary<int, Test> fasterDictionary = new FasterDictionary<int, Test>();
            Dictionary<int, Test> dictionary = new Dictionary<int, Test>();

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            System.Console.WriteLine("insert");
            for (int i = 0; i < dictionarysize; i++) dictionary[numbers[i]] = new Test(i);
            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) dictionary[numbers[i]] = new Test(i);
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);
            for (int i = 0; i < dictionarysize; i++) fasterDictionary[numbers[i]] = new Test(i);
            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) fasterDictionary[numbers[i]] = new Test(i); 
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);
/*
            fasterDictionary = new FasterDictionary<int, Test>();
            dictionary = new Dictionary<int, Test>();
            System.Console.WriteLine("add after new");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) dictionary.Add(numbers[i], new Test(i));
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) fasterDictionary.Add(numbers[i], new Test(i));
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);
            */
            fasterDictionary = new FasterDictionary<int, Test>(dictionarysize);
            dictionary = new Dictionary<int, Test>();
            System.Console.WriteLine("insert after new with presize");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) dictionary[numbers[i]] = new Test(i);
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) fasterDictionary[numbers[i]] = new Test(i);
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);

            dictionary.Clear();
            fasterDictionary.Clear();
            System.Console.WriteLine("insert after clear");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) dictionary[numbers[i]] = new Test(i);
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++) fasterDictionary[numbers[i]] = new Test(i);
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);

            System.Console.WriteLine("read");

            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++)
            {
                Test JapaneseCalendar;
                JapaneseCalendar = dictionary[numbers[i]];
            }

            watch.Stop();

            System.Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Reset();
            watch.Start();
            for (int i = 0; i < dictionarysize; i++)
            {
                Test JapaneseCalendar;
                JapaneseCalendar = fasterDictionary[numbers[i]];
            }

            watch.Stop();

            System.Console.WriteLine(watch.ElapsedMilliseconds);

            System.Console.WriteLine("iterate values");

            watch.Reset();
            watch.Start();
            for (int i = 0; i < 1; i++)
            {
                Test JapaneseCalendar;
                foreach (var VARIABLE in dictionary.Values)
                {
                    JapaneseCalendar = VARIABLE;
                }
            }

            watch.Stop();

            System.Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Reset();
            watch.Start();
            for (int i = 0; i < 1; i++)
            {
                Test JapaneseCalendar;
                int count;
                var buffer = fasterDictionary.GetValuesArray(out count);
                for (int j = 0; j < count; j++)
                {
                    JapaneseCalendar = buffer[j];
                }
            }

            watch.Stop();

            System.Console.WriteLine(watch.ElapsedMilliseconds);

            System.Console.WriteLine("remove");
            watch.Reset();
                        watch.Start();
                        for (int i = 0; i < dictionarysize; i++)
                        {
                            dictionary.Remove(numbers[i]);
                        }

                        watch.Stop();

                        System.Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Reset();
                        watch.Start();
                        for (int i = 0; i < dictionarysize; i++)
                        {
                            fasterDictionary.Remove(numbers[i]);
                        }

                        watch.Stop();

            System.Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DictionaryBenchmark>();
           //new DictionaryBenchmark().CheckCollisions();
           //DictionaryBenchmark.Profiling();
           //DictionaryBenchmark.Test2();
        }
    }
    
    

#if later

        static void Tests()
        {
            System.Console.WriteLine("it's happening");
            ThreadPool.QueueUserWorkItem(Test2);

            System.Console.ReadKey();
        }

      




      
#endif
    
}

#endif