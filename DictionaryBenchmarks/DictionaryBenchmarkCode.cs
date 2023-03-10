using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Svelto.DataStructures.Native;

namespace Svelto.DataStructures
{
    //[MonoJob("Mono x64", @"C:\Program Files\Mono\bin\mono.exe")]
    [SimpleJob(RunStrategy.Throughput, RuntimeMoniker.Net70)]

//    [InliningDiagnoser(true, false)] //what is not being inlined?
    //    [SimpleJob(RunStrategy.Monitoring, runtimeMoniker: RuntimeMoniker.NetCoreApp31, launchCount: 1,
    //              warmupCount: 1, targetCount: 1)] //this can be defined with multiple RunStrategy
    //[DisassemblyDiagnoser(printSource: true)] //checkasm
    // [EtwProfiler] //to profile the benchmark: https://benchmarkdotnet.org/articles/features/etwprofiler.html
    //   [HardwareCounters(HardwareCounter.BranchMispredictions,HardwareCounter.BranchInstructions)]
    [WarmupCount(3)]
    [IterationCount(10)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class DictionaryBenchmark
    {
        const int dictionarySize = 1_000_000;
        Native2.OldSveltoDictionaryNative<uint, Test> _nativeOldSveltoDictionary;
        SveltoDictionaryNative<uint, Test> _nativeSveltoDictionary;
        Dictionary<uint, Test> _standardDictionary;

        uint[] randomIndices;

        [GlobalSetup]
        public void StandardSetup()
        {
            randomIndices = new uint[dictionarySize];
            var r = new Random();
            for (int i = 0; i < dictionarySize; i++)
                randomIndices[i] = (uint)r.Next() % 100;

            _nativeOldSveltoDictionary = new Native2.OldSveltoDictionaryNative<uint, Test>(dictionarySize);
            _nativeSveltoDictionary = new SveltoDictionaryNative<uint, Test>(dictionarySize);
            _standardDictionary = new Dictionary<uint, Test>(dictionarySize);
        }

        [GlobalSetup(
            Targets = new[]
            {
                nameof(GetRandom),
                nameof(FasterGetRandom),
                nameof(OldGetRandom),
                nameof(RemoveRandom), nameof(FasterRemoveRandom), nameof(OldRemoveRandom)
                //, nameof(IterateValues), nameof(FasterIterateValues)
            })]
        public void GlobalSetupRandom()
        {
            randomIndices = new uint[dictionarySize];
            var r = new Random();
            for (int i = 0; i < dictionarySize; i++)
                randomIndices[i] = (uint)r.Next() % dictionarySize;
            
            _nativeOldSveltoDictionary = new Native2.OldSveltoDictionaryNative<uint, Test>(dictionarySize);
            _nativeSveltoDictionary = new SveltoDictionaryNative<uint, Test>(dictionarySize);
            _standardDictionary = new Dictionary<uint, Test>(dictionarySize);
            
            for (int i = 0; i < dictionarySize; i++)
                _standardDictionary[randomIndices[i]] = new Test(i);
            
            for (int i = 0; i < dictionarySize; i++)
                _nativeOldSveltoDictionary[randomIndices[i]] = new Test(i);
            
            for (int i = 0; i < dictionarySize; i++)
                _nativeSveltoDictionary[randomIndices[i]] = new Test(i);
        }

        //  [GlobalSetup(Targets = new[] { nameof(Get), nameof(FasterGet), nameof(OldGet)})]
        public void GlobalSetupGet()
        {
            //   fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            _standardDictionary = new Dictionary<uint, Test>(dictionarySize);
            for (int i = 0; i < dictionarySize; i++)
                _standardDictionary[(uint)i] = new Test(i);
            //   for (int i = 0; i < dictionarySize; i++) fasterDictionary[(uint) i] = new Test(i);
        }

        //  [GlobalSetup(Targets = new[] { nameof(Remove), nameof(FasterRemove), nameof(OldRemove) })]
        public void GlobalSetupRemove()
        {
            //   fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            _standardDictionary = new Dictionary<uint, Test>(dictionarySize);
            for (int i = 0; i < dictionarySize; i++)
                _standardDictionary[(uint)i] = new Test(i);
            //    for (int i = 0; i < dictionarySize; i++) fasterDictionary[(uint) i] = new Test(i);
        }

        [BenchmarkCategory("Insert"), Benchmark(Baseline = true)]
        public void StandardRandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++)
                _standardDictionary[randomIndices[i]] = new Test(i);
        }

        //  [BenchmarkCategory("Insert"), Benchmark]
        // public void FasterRandomInsert()
        // {
        //     for (int i = 0; i < dictionarySize; i++) fasterDictionary[randomIndices[i]] = new Test(i);
        // }

        [BenchmarkCategory("Insert"), Benchmark]
        public void OriginalSveltoRandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++)
                _nativeOldSveltoDictionary[randomIndices[i]] = new Test(i);
        }

        [BenchmarkCategory("Insert"), Benchmark]
        public void NewSveltoRandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++)
                _nativeSveltoDictionary[randomIndices[i]] = new Test(i);
        }

        //
        // [BenchmarkCategory("LinearInsert"), Benchmark(Baseline = true)]
        // public void LinearInsert()
        // {
        //     for (int i = 0; i < dictionarySize; i++) dictionary[(uint) i] = new Test(i);
        // }
        //
        // [BenchmarkCategory("LinearInsert"), Benchmark]
        // public void FasterLinearInsert()
        // {
        //     for (uint i = 0; i < dictionarySize; i++) fasterDictionary[i] = new Test((int) i);
        // }
        //
        // [BenchmarkCategory("LinearInsert"), Benchmark]
        // public void OldLinearInsert()
        // {
        //     for (uint i = 0; i < dictionarySize; i++) oldfasterDictionary[i] = new Test((int) i);
        // }
        //
        [BenchmarkCategory("GetRandom"), Benchmark(Baseline = true)]
        public void GetRandom()
        {
            Test X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X = _standardDictionary[randomIndices[i]];
                X.i = 1;
            }
        }

        [BenchmarkCategory("GetRandom"), Benchmark]
        public void FasterGetRandom()
        {
            Test X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X = _nativeSveltoDictionary[randomIndices[i]];
                X.i = 1;
            }
        }

        [BenchmarkCategory("GetRandom"), Benchmark]
        public void OldGetRandom()
        {
            Test X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X = _nativeOldSveltoDictionary[randomIndices[i]];
                X.i = 1;
            }
        }

//        [BenchmarkCategory("Get"), Benchmark(Baseline = true)]
//        public void Get()
//        {
//            Test X = default;
//            for (uint i = 0; i < dictionarySize; i++)
//            {
//                X = dictionary[i];
//                X.i = 1;
//            }
//        }
//
//        [BenchmarkCategory("Get"), Benchmark]
//        public void FasterGet()
//        {
//            Test X = default;
//            for (uint i = 0; i < dictionarySize; i++)
//            {
//                X = fasterDictionary[i];
//                X.i = 1;
//            }
//        }
//
//        [BenchmarkCategory("Get"), Benchmark]
//        public void OldGet()
//        {
//            Test X = default;
//            for (uint i = 0; i < dictionarySize; i++)
//            {
//                X = oldfasterDictionary[i];
//                X.i = 1;
//            }
//        }

        [BenchmarkCategory("RemoveRandom"), Benchmark(Baseline = true)]
        public void RemoveRandom()
        {
            for (int i = 0; i < randomIndices.Length; i++)
            {
                _standardDictionary.Remove(randomIndices[i]);
            }
        }

        [BenchmarkCategory("RemoveRandom"), Benchmark]
        public void FasterRemoveRandom()
        {
            for (int i = 0; i < randomIndices.Length; i++)
            {
                _nativeSveltoDictionary.Remove(randomIndices[i]);
            }
        }

        [BenchmarkCategory("RemoveRandom"), Benchmark]
        public void OldRemoveRandom()
        {
            for (int i = 0; i < randomIndices.Length; i++)
            {
                _nativeOldSveltoDictionary.Remove(randomIndices[i]);
            }
        }
        //
        // [BenchmarkCategory("Remove"), Benchmark(Baseline = true)]
        // public void Remove()
        // {
        //     for (uint i = 0; i < dictionarySize; i++)
        //     {
        //         dictionary.Remove(i);
        //     }
        // }
        //
        // [BenchmarkCategory("Remove"), Benchmark]
        // public void FasterRemove()
        // {
        //     for (uint i = 0; i < dictionarySize; i++)
        //     {
        //         fasterDictionary.Remove(i);
        //     }
        // }
        //
        // [BenchmarkCategory("Remove"), Benchmark]
        // public void OldRemove()
        // {
        //     for (uint i = 0; i < dictionarySize; i++)
        //     {
        //         oldfasterDictionary.Remove(i);
        //     }
        // }
        //
        // [IterationSetup(Targets = new[] { nameof(InsertFromEmtpy), 
        //     nameof(FasterInsertFromEmtpy),nameof(OldInsertFromEmtpy),
        //     nameof(LinearInsertFromEmtpy),
        //     nameof(FasterLinearInsertFromEmtpy), nameof(OldLinearInsertFromEmtpy) })]
        // public void IterationSetupEmpty()
        // {
        //     randomIndices = new uint[dictionarySize];
        //     for (int i = 0; i < dictionarySize; i++) randomIndices[i] = (uint) (i + i);
        //
        //     fasterDictionary = new FasterDictionary<uint, Test>();
        //     dictionary       = new Dictionary<uint, Test>();
        //     oldfasterDictionary = new OldFasterDictionary<uint, Test>();
        // }
        //
        // [IterationCleanup(Targets = new[] { nameof(InsertFromEmtpy), 
        //     nameof(FasterInsertFromEmtpy), nameof(OldInsertFromEmtpy),
        //     nameof(LinearInsertFromEmtpy),
        //     nameof(FasterLinearInsertFromEmtpy), nameof(OldLinearInsertFromEmtpy)  })]
        // public void IterationCleanEmpty()
        // {
        //     fasterDictionary.Clear();
        //     fasterDictionary.Trim();
        //     oldfasterDictionary.Clear();
        //     oldfasterDictionary.Trim();
        //     dictionary.Clear();
        // }
        //
        // [BenchmarkCategory("InsertFromEmpty"), Benchmark(Baseline = true)]
        // public void InsertFromEmtpy()
        // {
        //     for (int i = 0; i < dictionarySize; i++) dictionary.Add(randomIndices[i],  new Test(i));
        // }
        //
        // [BenchmarkCategory("InsertFromEmpty"), Benchmark]
        // public void FasterInsertFromEmtpy()
        // {
        //     for (int i = 0; i < dictionarySize; i++) fasterDictionary.Add(randomIndices[i],  new Test(i));
        // }
        //
        // [BenchmarkCategory("InsertFromEmpty"), Benchmark]
        // public void OldInsertFromEmtpy()
        // {
        //     for (int i = 0; i < dictionarySize; i++) oldfasterDictionary.Add(randomIndices[i],  new Test(i));
        // }
        //
        // [BenchmarkCategory("LinearInsertInsertFromEmpty"), Benchmark(Baseline = true)]
        // public void LinearInsertFromEmtpy()
        // {
        //     for (int i = 0; i < dictionarySize; i++) dictionary.Add((uint) i,  new Test(i));
        // }
        //
        // [BenchmarkCategory("LinearInsertInsertFromEmpty"), Benchmark]
        // public void FasterLinearInsertFromEmtpy()
        // {
        //     for (uint i = 0; i < dictionarySize; i++) fasterDictionary.Add(i, new Test((int) i));
        // }
        //
        // [BenchmarkCategory("LinearInsertInsertFromEmpty"), Benchmark]
        // public void OldLinearInsertFromEmtpy()
        // {
        //     for (uint i = 0; i < dictionarySize; i++) oldfasterDictionary.Add(i, new Test((int) i));
        // }
        //
        // [BenchmarkCategory("IterateValues"), Benchmark(Baseline = true)]
        // public void IterateValues()
        // {
        //     var dictionaryValues = dictionary.Values;
        //     foreach (var value in dictionaryValues)
        //     {
        //         var test = value;
        //         test.i = 1;
        //     }
        // }
        //
        // [BenchmarkCategory("IterateValues"), Benchmark]
        // public void FasterIterateValues()
        // {
        //     IBuffer<Test> readOnlyCollectionStruct = fasterDictionary.GetValuesArray();
        //     var count = readOnlyCollectionStruct.count;
        //     for (int i = 0; i < count; i++)
        //     {
        //         var test = readOnlyCollectionStruct[i];
        //         test.i = 1;
        //     }
        // }
        //
        // [BenchmarkCategory("IterateValues"), Benchmark]
        // public void FastestIterateValues()
        // {
        //     Test[] readOnlyCollectionStruct = fasterDictionary.GetValuesArray().ToManagedArray(out var count);
        //     for (int i = 0; i < count; i++)
        //     {
        //         var test = readOnlyCollectionStruct[i];
        //         test.i = 1;
        //     }
        // }
    }
}