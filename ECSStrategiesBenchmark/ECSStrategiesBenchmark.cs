using System;
using System.Numerics;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Svelto.DataStructures
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Test
    {
        public Vector3 position;
        public Vector4 rotation;
        public Vector3 velocity;
    }

    //[DisassemblyDiagnoser(printAsm: true, printSource: true)] // !!! use the new diagnoser!!
    [MonoJob]
    //[RyuJitX64Job]
    // [EtwProfiler]
    //   [HardwareCounters(HardwareCounter.BranchMispredictions,HardwareCounter.BranchInstructions)]
    //[MaxWarmupCount(2)]
    //[MaxIterationCount(2)]
    //[MinWarmupCount(1)]
    //[MinIterationCount(1)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class ECSStrategiesBenchmark
    {
        const int dictionarySize = 1000;

        Vector3[][] _positionsSeparate;
        Vector4[][] _rotationsSeparate;
        Vector3[][] _velocitiesSeparate;
        
        Vector3[] _positionsSet;
        Vector4[] _rotationsSet;
        Vector3[] _velocitiesSet;
        
        Test[][] _separateArraysStrategy; 
        int[] _sparseSet1;
        int[] _sparseSet2;
        
        Test[] _sparseSetEntities;
        int CACHE_MISSES = 500;

        [GlobalSetup]
        public void StandardSetup()
        {
            _separateArraysStrategy = new Test[CACHE_MISSES][];

            for (int i = 0; i < CACHE_MISSES; i++)
            {
                _separateArraysStrategy[i] = new Test[dictionarySize];
            }
            
            _positionsSeparate = new Vector3[CACHE_MISSES][];
                _rotationsSeparate = new Vector4[CACHE_MISSES][];
            _velocitiesSeparate = new Vector3[CACHE_MISSES][];
            for (int i = 0; i < CACHE_MISSES; i++)
            {
                _positionsSeparate[i] = new Vector3[dictionarySize];
                _rotationsSeparate[i] = new Vector4[dictionarySize];
                _velocitiesSeparate[i] = new Vector3[dictionarySize];
            }
            
            _sparseSet1 = new int[dictionarySize * CACHE_MISSES];
            _sparseSet2 = new int[dictionarySize * CACHE_MISSES];
            _sparseSetEntities = new Test[dictionarySize * CACHE_MISSES];
            
            _positionsSet = new Vector3[dictionarySize * CACHE_MISSES];
            _rotationsSet = new Vector4[dictionarySize * CACHE_MISSES];
            _velocitiesSet = new Vector3[dictionarySize * CACHE_MISSES];

            for (int i = 0; i < dictionarySize * CACHE_MISSES; i++)
            {
                _sparseSet1[i] = _sparseSet2[i] = i;
                _sparseSetEntities[i] = new Test();
            }
        }

        [Benchmark, BenchmarkCategory("SOA")]
        public void SeparateArraysStrategyReal()
        {
            for (int i = 0; i < CACHE_MISSES; i++)
            {
                var vector3s = _positionsSeparate[i];
                var vector3s1 = _velocitiesSeparate[i];
                var vector4s = _rotationsSeparate[i];
                
                for (int j = 0; j < dictionarySize; j++)
                {
                    vector3s[j] = vector3s1[j];
                    vector4s[j]= new Vector4(2, 2, 2, 2);
                }
            }
        }
        
        [Benchmark, BenchmarkCategory("AOS")]
        public void SeparateArraysStrategy()
        {
            var separateArraysStrategy = _separateArraysStrategy;
            
            for (int i = 0; i < CACHE_MISSES; i++)
            {
                for (int j = 0; j < dictionarySize; j++)
                {
                    separateArraysStrategy[i][j].position = separateArraysStrategy[i][j].velocity;
                    separateArraysStrategy[i][j].rotation = new Vector4(2, 2, 2, 2);
                }
            }
        }
        
        [Benchmark, BenchmarkCategory("SOA")]
        public void SparseSetStrategy()
        {
            var sparseSetEntities = _sparseSetEntities;
            var sparseSet1 = _sparseSet1;
            var sparseSet2 = _sparseSet2;
            
            for (int j = 0; j < dictionarySize * CACHE_MISSES; j++)
            {
                sparseSetEntities[j].position = sparseSetEntities[sparseSet1[j]].velocity;
                sparseSetEntities[sparseSet2[j]].rotation = new Vector4(2, 2, 2, 2);
            }
        }
        
        [Benchmark, BenchmarkCategory("AOS")]
        public void SparseSetStrategyReal()
        {
            var sparseSet1 = _sparseSet1;
            var sparseSet2 = _sparseSet2;
            
            for (int j = 0; j < dictionarySize * CACHE_MISSES; j++)
            {
                _positionsSet[j] = _velocitiesSet[sparseSet2[j]];
                _rotationsSet[sparseSet1[j]] = new Vector4(2, 2, 2, 2);
            }
        }
        
        [Benchmark, BenchmarkCategory("SOA")]
        public void CopyStrategy()
        {
            var sparseSetEntities = _sparseSetEntities;
            var separateArraysStrategy = _separateArraysStrategy;
            
            for (int i = 0; i < CACHE_MISSES; i++)
                Array.Copy(sparseSetEntities, i * CACHE_MISSES, separateArraysStrategy[i], 0, dictionarySize);
            
            for (int j = 0; j < dictionarySize * CACHE_MISSES; j++)
            {
                sparseSetEntities[j].position = sparseSetEntities[j].velocity;
                sparseSetEntities[j].rotation = new Vector4(2, 2, 2, 2);
            }
            
            for (int i = 0; i < CACHE_MISSES; i++)
                Array.Copy(separateArraysStrategy[i], 0, sparseSetEntities, i * CACHE_MISSES, dictionarySize);
        }
        
        [Benchmark, BenchmarkCategory("AOS")]
        public void CopyStrategyReal()
        {
            for (int i = 0; i < CACHE_MISSES; i++)
            {
                Array.Copy(_positionsSet, i * CACHE_MISSES, _positionsSeparate[i], 0, dictionarySize);
                Array.Copy(_rotationsSet, i * CACHE_MISSES, _rotationsSeparate[i], 0, dictionarySize);
                Array.Copy(_velocitiesSet, i * CACHE_MISSES, _velocitiesSeparate[i], 0, dictionarySize);
            }
            
            for (int j = 0; j < dictionarySize * CACHE_MISSES; j++)
            {
                _positionsSet[j] = _velocitiesSet[j];
                _rotationsSet[j] = new Vector4(2, 2, 2, 2);
            }
            
            
            for (int i = 0; i < CACHE_MISSES; i++)
            {
                Array.Copy(_positionsSeparate[i], 0, _positionsSet, i * CACHE_MISSES, dictionarySize);
                Array.Copy(_rotationsSeparate[i], 0, _rotationsSet, i * CACHE_MISSES, dictionarySize);
                Array.Copy(_velocitiesSeparate[i], 0, _velocitiesSet, i * CACHE_MISSES, dictionarySize);
            }
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ECSStrategiesBenchmark>();
        }
    }
}
