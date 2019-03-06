using System.Collections.Generic;

namespace Svelto.Tasks
{
    struct SveltoTaskWrapper<TTask, TRunner>
        where TTask : IEnumerator<TaskContract> where TRunner : class, IRunner<Lean.SveltoTask<TTask>>
    {
        public SveltoTaskWrapper(ref TTask task, TRunner runner) : this()
        {
            _taskContinuation._runner = runner;
            this.task = task;
        }

        public bool MoveNext()
        {
            if (_current.Continuation != null)
            {
                //a task is waiting to be completed, spin this one
                if (_current.Continuation.Value.isRunning == true)
                    return true;

                //this is a continued task
                if (_taskContinuation._continuingTask != null)
                {
                    //the child task is telling to interrupt everything!
                    var currentBreakIt = _taskContinuation._continuingTask.Current.breakIt;
                    _taskContinuation._continuingTask = null;

                    if (currentBreakIt == Break.AndStop)
                        return false;
                }
            }
            
            if (_current.enumerator != null)
            {
                if (_current.isTaskEnumerator)
                {
                    _taskContinuation._continuingTask = (IEnumerator<TaskContract>) _current.enumerator;

                    //a new TaskContract is created, holding the continuationEnumerator of the new task
                    var continuation =
                        ((TTask) _taskContinuation._continuingTask).RunImmediate(_taskContinuation._runner);

                    _current = continuation.isRunning == true ? new TaskContract(continuation) : new TaskContract();

                    return true;
                }
                else
                {
                    if (_current.enumerator.MoveNext() == true)
                        return true;

                    _current = new TaskContract();
                }
            }

            //continue the normal execution of this task
            if (task.MoveNext() == false)
                return false;

            _current = task.Current;

            if (_current.yieldIt == true)
                return true;

            if (_current.breakIt == Break.It || _current.breakIt == Break.AndStop || _current.hasValue == true)
                return false;

            return true;
        }

        internal TTask task { get; }

        ContinueTask _taskContinuation;
        TaskContract _current;

        struct ContinueTask
        {
            internal TRunner                   _runner;
            internal IEnumerator<TaskContract> _continuingTask;
        }
    }
}