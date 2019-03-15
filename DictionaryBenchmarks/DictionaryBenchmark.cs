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
        FasterDictionary<Test> fasterDictionaryInt;
        FasterDictionary<uint, Test> fasterDictionary;
        Dictionary<uint, Test>       dictionary;
        uint[]                       randomIndices;
        Test[] tests;

        [GlobalSetup]
        public void GlobalSetup()
        {
            dictionarysize = 10000000;

            randomIndices = new uint[dictionarysize];
            tests = new Test[dictionarysize];
            var r = new Random();
            for (int i = 0; i < dictionarysize; i++) randomIndices[i] = (uint) r.Next();
            for (int i = 0; i < dictionarysize; i++) tests[i] = new Test(i);

            fasterDictionary = new FasterDictionary<uint, Test>(dictionarysize);
            fasterDictionaryInt = new FasterDictionary<Test>(dictionarysize);
            dictionary = new Dictionary<uint, Test>(dictionarysize);
        }

        [GlobalCleanup]
        public void GlobalCleanUp()
        {}
        
        [BenchmarkCategory("Insert"), Benchmark(Baseline = true)]
        public void StandardDictionaryInsert()
        {
            for (int i = 0; i < dictionarysize; i++) dictionary[randomIndices[i]] = new Test(i);
        }
    
        [BenchmarkCategory("Insert"), Benchmark]
        public void FasterDictionaryInsert()
        {
            for (int i = 0; i < dictionarysize; i++) fasterDictionary[randomIndices[i]] = new Test(i);
        }
        
        [BenchmarkCategory("Insert"), Benchmark]
        public void FasterDictionaryIntInsert()
        {
            for (int i = 0; i < dictionarysize; i++) fasterDictionaryInt[(int) randomIndices[i]] = new Test(i);
        }
        
       // [BenchmarkCategory("Get"), Benchmark(Baseline = true)]
        public void StandardGet()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
                X = dictionary[randomIndices[i]];
            }
        }

      //  [BenchmarkCategory("Get"), Benchmark]
        public void NewGet()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
                X = fasterDictionary[randomIndices[i]];
            }
        }
        
    //    [BenchmarkCategory("Get"), Benchmark]
        public void NewGet2()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
       //         X = fasterDictionary2[numbers[i]];
            }
        }
        
//        [BenchmarkCategory("Get"), Benchmark]
        public void NewGet3()
        {
            Test X;
            for (int i = 0; i < dictionarysize; i++)
            {
                X = tests[randomIndices[i]];
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
            //BenchmarkRunner.Run<DictionaryBenchmark>();
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance);

            var fasterDictionaryInt = new FasterDictionary<Test>(1000);
            for (int i = 0; i < 1000; i++) fasterDictionaryInt[i] = new Test(i);
        }
    }
}
