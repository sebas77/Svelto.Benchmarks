using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Svelto.Tasks;
using Svelto.Tasks.Internal;

namespace Benchmarks
{
    public struct UltraSpecializedEnumerator : IEnumerator<TaskContract>
    {
        int i;

        public bool MoveNext()
        {
            if (i++ < 1000)
                return true;

            return false;
        }

        public void Reset()
        {}

        TaskContract IEnumerator<TaskContract>.Current { get; }

        public object Current { get; }
        public void Dispose() {}
    }
    
    public class UItraSpecializedEnumeratorClass : IEnumerator
    {
        int i;

        public bool MoveNext()
        {
            if (i++ < 1000)
                return true;

            return false;
        }

        public void Reset()
        {}

        public object Current { get; }
    }
    
    public class UpdateMonoRunner<T> : BaseRunner<T> where T: ISveltoTask
    {
        public UpdateMonoRunner(string name, int size):base(name, size)
        {
            var info = new CoroutineRunner<T>.StandardRunningTasksInfo { runnerName = name };

            _processEnumerator = new CoroutineRunner<T>.Process<CoroutineRunner<T>.StandardRunningTasksInfo>
                (_newTaskRoutines, _coroutines, _flushingOperation, info);
        }

        public UpdateMonoRunner(string name) : base(name)
        {
            var info = new CoroutineRunner<T>.StandardRunningTasksInfo { runnerName = name };

            _processEnumerator = new CoroutineRunner<T>.Process<CoroutineRunner<T>.StandardRunningTasksInfo>
                (_newTaskRoutines, _coroutines, _flushingOperation, info);
        }
    }

    [SimpleJob(RunStrategy.Monitoring, launchCount: 1, warmupCount: 3, targetCount: 4)]
    //[DisassemblyDiagnoser(printAsm: true, printSource: true)] // !!! use the new diagnoser!!
    //[RyuJitX64Job]
   // [EtwProfiler]
 //   [HardwareCounters(HardwareCounter.BranchMispredictions,HardwareCounter.BranchInstructions)]
    public class Program2
    {
        readonly SyncRunner<ExtraLeanSveltoTask<UltraSpecializedEnumerator>> runner;
        readonly SyncRunner<ExtraLeanSveltoTask<UItraSpecializedEnumeratorClass>> runner2;
        readonly UpdateMonoRunner<ExtraLeanSveltoTask<UltraSpecializedEnumerator>> runner3;
        readonly UpdateMonoRunner<LeanSveltoTask<UltraSpecializedEnumerator>> runner4;
        readonly UpdateMonoRunner<LeanSveltoTask<IEnumerator<TaskContract>>> runner5;

        public Program2()
        {
            runner = new SyncRunner<ExtraLeanSveltoTask<UltraSpecializedEnumerator>>(-1);
            runner2 = new SyncRunner<ExtraLeanSveltoTask<UItraSpecializedEnumeratorClass>>(-1);
            runner3 = new UpdateMonoRunner<ExtraLeanSveltoTask<UltraSpecializedEnumerator>>("test");
            runner4 = new UpdateMonoRunner<LeanSveltoTask<UltraSpecializedEnumerator>>("test2", 100000);
            runner5 = new UpdateMonoRunner<LeanSveltoTask<IEnumerator<TaskContract>>>("test3", 100000);
        }

        [Benchmark]
        public void TestLeanUpdateSpecializedRunner()
        {
            for (int i = 0; i < 100000; i++)
                new UltraSpecializedEnumerator().Run(runner4);
            
            while (runner4.numberOfProcessingTasks > 0)
                runner4.Step();
        }
        
       [Benchmark]
        public void TestExtraUpdateSpecializedRunner()
        {
            for (int i = 0; i < 100000; i++)
                new UltraSpecializedEnumerator().Start(runner3);
            
            while (runner3.numberOfProcessingTasks > 0)
                runner3.Step();
        }
        
       [Benchmark]
        public void TestUltraSpecializedRunner()
        {
            for (int i = 0; i < 100000; i++)
                new UltraSpecializedEnumerator().Complete();
        }
        
        [Benchmark]
        public void TestUltraSpecializedRunnerClass()
        {
            for (int i = 0; i < 100000; i++)
                new UItraSpecializedEnumeratorClass().Complete();
        }
        
        [Benchmark]
        public void TestUltraSpecializedRunnerClass5()
        {
            IEnumerator<TaskContract> UltraSpecializedEnumeratorInner()
            {
                int i = 0;

                while (i++ < 1000)
                    yield return Yield.It;
            }
            
            for (int i = 0; i < 100000; i++)
            {
                var ultraSpecializedEnumeratorInner = UltraSpecializedEnumeratorInner();
                ultraSpecializedEnumeratorInner.Run(runner5);
            }

            while (runner5.numberOfProcessingTasks > 0)
                runner5.Step();
        }

        [Benchmark(Description = "JustRunIt", Baseline = true)]
        public void JustRunIt()
        {
            for (int i = 0; i < 100000; i++)
            {
                var test = new UltraSpecializedEnumerator?(new UltraSpecializedEnumerator());
                var testValue = test.Value;
                while (testValue.MoveNext()) ;
            }
        }
    }

    public class Program
        {
        public static void Main(string[] args)
        {
#if RELEASE
            var summary = BenchmarkRunner.Run<Program2>();
#else
            new Program2().TestLeanUpdateSpecializedRunner();
#endif
        }
    }
}