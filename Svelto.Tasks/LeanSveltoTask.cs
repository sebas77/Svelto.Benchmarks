#if ENABLE_PLATFORM_PROFILER || TASKS_PROFILER_ENABLED || (DEBUG && !PROFILER)
#define GENERATE_NAME
#endif

using System;
using System.Collections.Generic;
using Svelto.Tasks.Enumerators;
using Svelto.Tasks.Internal;

namespace Svelto.Tasks.Lean
{
    public struct SveltoTask<TTask>: ISveltoTask where TTask : IEnumerator<TaskContract>
    {
        internal ContinuationEnumerator Run<TRunner>(TRunner   runner,
                                                       ref TTask task/*,
                                                       bool      immediate*/)
            where TRunner : class, IRunner<SveltoTask<TTask>>
        {
#if DEBUG && !PROFILER                        
            DBC.Tasks.Check.Require(IS_TASK_STRUCT == true || task != null, 
                                    "A valid enumerator is required to enable a LeanSveltTask ".FastConcat(ToString()));
   
            DBC.Tasks.Check.Require(runner != null, "SetScheduler function has never been called");
#endif            
    
            _continuationEnumerator = new ContinuationEnumerator(ContinuationPool.RetrieveFromPool());
            _sveltoTask = new SveltoTaskWrapper<TTask, IRunner<SveltoTask<TTask>>>(ref task, runner);
            _threadSafeSveltoTaskStates.started = true;
            
            runner.StartCoroutine(ref this/*, immediate*/);

            return _continuationEnumerator;
        }
        
        public override string ToString()
        {
#if GENERATE_NAME
            if (_name == null)
                _name = base.ToString();
    
            return _name;
#else
            return "LeanSveltoTask";
#endif            
        }

        public void Stop()
        {
            _threadSafeSveltoTaskStates.explicitlyStopped = true;
        }

        TaskContract ISveltoTask.Current => throw new Exception();
        TTask Current => _sveltoTask.task;

        public bool MoveNext()
        {
            DBC.Tasks.Check.Require(_threadSafeSveltoTaskStates.completed == false,
                                    "impossible state ".FastConcat(ToString()));
            bool completed = false;
            try
            {
                if (_threadSafeSveltoTaskStates.explicitlyStopped == false)
                {
                    try
                    {
                        completed = !_sveltoTask.MoveNext();
                    }
                    catch (Exception e)
                    {
                        completed = true;

                        Console.LogException("a Svelto.Tasks task threw an exception at:  ".FastConcat(ToString()), e);

                        throw;
                    }
                }
                else
                    completed = true;
            }
            finally
            {
                if (completed == true)
                {
                    _continuationEnumerator.ce.ReturnToPool();
                    _threadSafeSveltoTaskStates.completed = true;
                }                
            }

            return !completed;
        }

        SveltoTaskWrapper<TTask, IRunner<SveltoTask<TTask>>> _sveltoTask;
        SveltoTaskState                                      _threadSafeSveltoTaskStates;
        ContinuationEnumerator                               _continuationEnumerator;
#if GENERATE_NAME
        string _name;
#endif
#if DEBUG && !PROFILER
        static readonly bool IS_TASK_STRUCT = typeof(TTask).IsValueType;
#endif

    }
}