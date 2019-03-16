using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
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
    [MonoJob, RyuJitX64Job]
    //[RyuJitX64Job]
    // [EtwProfiler]
    //   [HardwareCounters(HardwareCounter.BranchMispredictions,HardwareCounter.BranchInstructions)]
    //[MaxWarmupCount(2)]
    //[MaxIterationCount(2)]
    //[MinWarmupCount(1)]
    //[MinIterationCount(1)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class DictionaryBenchmark
    {
        const int                    dictionarySize = 10_000_000;
        FasterDictionary<uint, Test> fasterDictionary;
        Dictionary<uint, Test>       dictionary;
        uint[]                       randomIndices;

        public DictionaryBenchmark()
        {
            randomIndices = new uint[dictionarySize];
            var r                                                     = new Random();
            for (int i = 0; i < dictionarySize; i++) randomIndices[i] = (uint) r.Next();
        }

        [GlobalSetup]
        public void StandardSetup()
        {
            fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            dictionary = new Dictionary<uint, Test>(dictionarySize);
        }
        
        [GlobalSetup(Targets = new[]
        {
            nameof(GetRandom), nameof(FasterGetRandom), nameof(IterateValues), nameof(FasterIterateValues),
            
        })]
        public void GlobalSetupRandom()
        {
            fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            dictionary       = new Dictionary<uint, Test>(dictionarySize);
            for (int i = 0; i < dictionarySize; i++) dictionary[randomIndices[i]]       = new Test(i);
            for (int i = 0; i < dictionarySize; i++) fasterDictionary[randomIndices[i]] = new Test(i);
        }
        
        [GlobalSetup(Targets = new[]
        {
            nameof(RemoveRandom), nameof(FasterRemoveRandom),
        })]
        public void GlobalSetupRemoveRandom()
        {
            fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            dictionary       = new Dictionary<uint, Test>(dictionarySize);
            for (int i = 0; i < dictionarySize; i++) dictionary[randomIndices[i]]       = new Test(i);
            for (int i = 0; i < dictionarySize; i++) fasterDictionary[randomIndices[i]] = new Test(i);
        }
        
        [GlobalSetup(Targets = new[] { nameof(Get), nameof(FasterGet)})]
        public void GlobalSetupGet()
        {
            fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            dictionary       = new Dictionary<uint, Test>(dictionarySize);
            for (int i = 0; i < dictionarySize; i++) dictionary[(uint) i]       = new Test(i);
            for (int i = 0; i < dictionarySize; i++) fasterDictionary[(uint) i] = new Test(i);
        }
        
        [GlobalSetup(Targets = new[] { nameof(Remove), nameof(FasterRemove) })]
        public void GlobalSetupRemove()
        {
            fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            dictionary       = new Dictionary<uint, Test>(dictionarySize);
            for (int i = 0; i < dictionarySize; i++) dictionary[(uint) i]       = new Test(i);
            for (int i = 0; i < dictionarySize; i++) fasterDictionary[(uint) i] = new Test(i);
        }


        [BenchmarkCategory("Insert"), Benchmark(Baseline = true)]
        public void RandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++) dictionary[randomIndices[i]] = new Test(i);
        }
    
        [BenchmarkCategory("Insert"), Benchmark]
        public void FasterRandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++) fasterDictionary[randomIndices[i]] = new Test(i);
        }
        
        [BenchmarkCategory("LinearInsert"), Benchmark(Baseline = true)]
        public void LinearInsert()
        {
            for (int i = 0; i < dictionarySize; i++) dictionary[(uint) i] = new Test(i);
        }
    
        [BenchmarkCategory("LinearInsert"), Benchmark]
        public void FasterLinearInsert()
        {
            for (uint i = 0; i < dictionarySize; i++) fasterDictionary[i] = new Test((int) i);
        }

        [BenchmarkCategory("GetRandom"), Benchmark(Baseline = true)]
        public void GetRandom()
        {
            Test X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X = dictionary[randomIndices[i]];
                X.i = 1;
            }
        }

        [BenchmarkCategory("GetRandom"), Benchmark]
        public void FasterGetRandom()
        {
            Test X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X = fasterDictionary[randomIndices[i]];
                X.i = 1;
            }
        }
        
        [BenchmarkCategory("Get"), Benchmark(Baseline = true)]
        public void Get()
        {
            Test X = default;
            for (uint i = 0; i < dictionarySize; i++)
            {
                X   = dictionary[i];
                X.i = 1;
            }
        }

        [BenchmarkCategory("Get"), Benchmark]
        public void FasterGet()
        {
            Test X = default;
            for (uint i = 0; i < dictionarySize; i++)
            {
                X   = fasterDictionary[i];
                X.i = 1;
            }
        }
        
        [BenchmarkCategory("RemoveRandom"), Benchmark(Baseline = true)]
        public void RemoveRandom()
        {
            for (int i = 0; i < randomIndices.Length; i++)
            {
                dictionary.Remove(randomIndices[i]);
            }
        }

        [BenchmarkCategory("RemoveRandom"), Benchmark]
        public void FasterRemoveRandom()
        {
            for (int i = 0; i < randomIndices.Length; i++)
            {
                fasterDictionary.Remove(randomIndices[i]);
            }
        }
        
        [BenchmarkCategory("Remove"), Benchmark(Baseline = true)]
        public void Remove()
        {
            for (uint i = 0; i < dictionarySize; i++)
            {
                dictionary.Remove(i);
            }
        }

        [BenchmarkCategory("Remove"), Benchmark]
        public void FasterRemove()
        {
            for (uint i = 0; i < dictionarySize; i++)
            {
                fasterDictionary.Remove(i);
            }
        }
        
        [IterationSetup(Targets = new[] { nameof(InsertFromEmtpy), 
            nameof(FasterInsertFromEmtpy),
            nameof(LinearInsertFromEmtpy),
            nameof(FasterLinearInsertFromEmtpy) })]
        public void IterationSetupEmpty()
        {
            randomIndices = new uint[dictionarySize];
            for (int i = 0; i < dictionarySize; i++) randomIndices[i] = (uint) (i + i);

            fasterDictionary = new FasterDictionary<uint, Test>();
            dictionary       = new Dictionary<uint, Test>();
        }
        
        [IterationCleanup(Targets = new[] { nameof(InsertFromEmtpy), 
            nameof(FasterInsertFromEmtpy),
            nameof(LinearInsertFromEmtpy),
            nameof(FasterLinearInsertFromEmtpy) })]
        public void IterationCleanEmpty()
        {
            fasterDictionary.Clear();
            fasterDictionary.Trim();
            dictionary.Clear();
        }
        
        [BenchmarkCategory("InsertFromEmpty"), Benchmark(Baseline = true)]
        public void InsertFromEmtpy()
        {
            for (int i = 0; i < dictionarySize; i++) dictionary.Add(randomIndices[i],  new Test(i));
        }
    
        [BenchmarkCategory("InsertFromEmpty"), Benchmark]
        public void FasterInsertFromEmtpy()
        {
            for (int i = 0; i < dictionarySize; i++) fasterDictionary.Add(randomIndices[i],  new Test(i));
        }
        
        [BenchmarkCategory("LinearInsertInsertFromEmpty"), Benchmark(Baseline = true)]
        public void LinearInsertFromEmtpy()
        {
            for (int i = 0; i < dictionarySize; i++) dictionary.Add((uint) i,  new Test(i));
        }
    
        [BenchmarkCategory("LinearInsertInsertFromEmpty"), Benchmark]
        public void FasterLinearInsertFromEmtpy()
        {
            for (uint i = 0; i < dictionarySize; i++) fasterDictionary.Add(i, new Test((int) i));
        }
        
        [BenchmarkCategory("IterateValues"), Benchmark(Baseline = true)]
        public void IterateValues()
        {
            var dictionaryValues = dictionary.Values;
            foreach (var value in dictionaryValues)
            {
                var test = value;
                test.i = 1;
            }
        }
    
        [BenchmarkCategory("IterateValues"), Benchmark]
        public void FasterIterateValues()
        {
            var readOnlyCollectionStruct = fasterDictionary.GetValuesArray(out int count);
            for (int i = 0; i < count; i++)
            {
                var test = readOnlyCollectionStruct[i];
                test.i = 1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DictionaryBenchmark>();
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);
        }
    }
}
