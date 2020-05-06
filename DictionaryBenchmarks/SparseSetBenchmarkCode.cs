#if later
using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Svelto.DataStructures
{
    [MonoJob]
    //[RyuJitX64Job]
    // [EtwProfiler]
    //   [HardwareCounters(HardwareCounter.BranchMispredictions,HardwareCounter.BranchInstructions)]
    [MaxWarmupCount(2)]
    [MaxIterationCount(2)]
    [MinWarmupCount(1)]
    [MinIterationCount(1)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class SparseSetBenchmark
    {
        const int                    dictionarySize = 10_000_000;
        FasterSparseSet<float>        sparseSet;
        FasterDictionary<uint, float> dictionary;
        int[] array;
        
        readonly uint[] randomIndices;
        float[] array2;

        public SparseSetBenchmark()
        {
            randomIndices = new uint[dictionarySize];
            var r                                                     = new Random();
            for (int i = 0; i < dictionarySize; i++) randomIndices[i] = (uint) r.Next() % dictionarySize;
        }

        [GlobalSetup(Targets = new[]
        {
            nameof(RandomInsert), nameof(FasterRandomInsert),nameof(FastestRandomInsert)
        })]
        public void StandardSetup()
        {
            sparseSet = new FasterSparseSet<float>(dictionarySize);
            dictionary       = new FasterDictionary<uint, float>(dictionarySize);
            array = new int[dictionarySize];
            array2 = new float[dictionarySize];
        }
        [GlobalSetup(Targets = new[]
        {
            nameof(GetRandom), nameof(FasterGetRandom), nameof(FastestGetRandom), 
            //nameof(IterateValues), nameof(FasterIterateValues),
            
        })]
        public void GlobalSetupRandom()
        {
            sparseSet = new FasterSparseSet<float>(dictionarySize);
            dictionary       = new FasterDictionary<uint, float>(dictionarySize);
            array  = new int[dictionarySize];
            array2 = new float[dictionarySize];

            for (int i = 0; i < dictionarySize; i++) dictionary[randomIndices[i]]  = i;
            for (int i = 0; i < dictionarySize; i++) sparseSet.Set(randomIndices[i], i);
            for (int i = 0; i < dictionarySize; i++) array2[array[randomIndices[i]]] = i;
            
        }
        /* 
         
         [GlobalSetup(Targets = new[]
         {
             nameof(RemoveRandom), nameof(FasterRemoveRandom),
         })]
         public void GlobalSetupRemoveRandom()
         {
             sparseSet = new FasterSparseSet<float>(dictionarySize);
             dictionary       = new SparseSet<uint, float>(dictionarySize);
             for (int i = 0; i < dictionarySize; i++) dictionary[randomIndices[i]]       = new float(i);
             for (int i = 0; i < dictionarySize; i++) sparseSet[randomIndices[i]] = new float(i);
         }
         
         [GlobalSetup(Targets = new[] { nameof(Get), nameof(FasterGet)})]
         public void GlobalSetupGet()
         {
             sparseSet = new FasterSparseSet<float>(dictionarySize);
             dictionary       = new SparseSet<uint, float>(dictionarySize);
             for (int i = 0; i < dictionarySize; i++) dictionary[(uint) i]       = new float(i);
             for (int i = 0; i < dictionarySize; i++) sparseSet[(uint) i] = new float(i);
         }
         
         [GlobalSetup(Targets = new[] { nameof(Remove), nameof(FasterRemove) })]
         public void GlobalSetupRemove()
         {
             sparseSet = new FasterSparseSet<float>(dictionarySize);
             dictionary       = new SparseSet<uint, float>(dictionarySize);
             for (int i = 0; i < dictionarySize; i++) dictionary[(uint) i]       = new float(i);
             for (int i = 0; i < dictionarySize; i++) sparseSet[(uint) i] = new float(i);
         }
 
 */
    //    [BenchmarkCategory("Insert"), Benchmark(Baseline = true)]
        public void RandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++) dictionary[randomIndices[i]] = 0f;
        }
    
      //  [BenchmarkCategory("Insert"), Benchmark]
        public void FasterRandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++) sparseSet[randomIndices[i]] = 0f;
        }
        
    //    [BenchmarkCategory("Insert"), Benchmark]
        public void FastestRandomInsert()
        {
            for (int i = 0; i < dictionarySize; i++) array2[array[randomIndices[i]]] = 0f;
        }
      /*  
        [BenchmarkCategory("LinearInsert"), Benchmark(Baseline = true)]
        public void LinearInsert()
        {
            for (int i = 0; i < dictionarySize; i++) dictionary[(uint) i] = new float(i);
        }
    
        [BenchmarkCategory("LinearInsert"), Benchmark]
        public void FasterLinearInsert()
        {
            for (uint i = 0; i < dictionarySize; i++) sparseSet[i] = new float((int) i);
        }
*/
        [BenchmarkCategory("GetRandom"), Benchmark(Baseline = true)]
        public void GetRandom()
        {
            float X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X   = dictionary[randomIndices[i]];
                X++;
            }
        }

        [BenchmarkCategory("GetRandom"), Benchmark]
        public void FasterGetRandom()
        {
            float X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X   = sparseSet[randomIndices[i]];
                X++;
            }
        }
        
        [BenchmarkCategory("GetRandom"), Benchmark]
        public void FastestGetRandom()
        {
            float X = default;
            for (int i = 0; i < dictionarySize; i++)
            {
                X = array2[array[randomIndices[i]]];
                X++;
            }
        }
        /*
        [BenchmarkCategory("Get"), Benchmark(Baseline = true)]
        public void Get()
        {
            float X = default;
            for (uint i = 0; i < dictionarySize; i++)
            {
                X   = dictionary[i];
                X.i = 1;
            }
        }

        [BenchmarkCategory("Get"), Benchmark]
        public void FasterGet()
        {
            float X = default;
            for (uint i = 0; i < dictionarySize; i++)
            {
                X   = sparseSet[i];
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
                sparseSet.Remove(randomIndices[i]);
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
                sparseSet.Remove(i);
            }
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
            var readOnlyCollectionStruct = sparseSet.GetValuesArray(out var count);
            for (int i = 0; i < count; i++)
            {
                var test = readOnlyCollectionStruct[i];
                test.i = 1;
            }
        }*/
    }
}
#endif