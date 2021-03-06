#if UNITY_5 || UNITY_5_3_OR_NEWER
using System.Collections;
using System.Collections.Generic;
using Svelto.Common;
using Svelto.Tasks.Internal;
using Svelto.Tasks.Unity.Internal;

namespace Svelto.Tasks
{
    namespace Lean.Unity
    {
        public class UpdateMonoRunner:UpdateMonoRunner<IEnumerator<TaskContract>>
        {
            public UpdateMonoRunner(string name) : base(name)
            {
            }
        }
        
        public class UpdateMonoRunner<T> : Svelto.Tasks.Unity.UpdateMonoRunner<SveltoTask<T>> where T : IEnumerator<TaskContract>
        {
            public UpdateMonoRunner(string name) : base(name)
            {
            }
        }
    }
    
    namespace ExtraLean.Unity
    {
        public class UpdateMonoRunner:UpdateMonoRunner<IEnumerator>
        {
            public UpdateMonoRunner(string name) : base(name)
            {
            }
        }
        
        public class UpdateMonoRunner<T> : Svelto.Tasks.Unity.UpdateMonoRunner<SveltoTask<T>> where T : IEnumerator
        {
            public UpdateMonoRunner(string name) : base(name)
            {
            }
        }
    }

    namespace Unity
    {
        public class UpdateMonoRunner<T> : UpdateMonoRunner<T, StandardRunningTasksInfo> where T : ISveltoTask
        {
            public UpdateMonoRunner(string name) : base(name, new StandardRunningTasksInfo())
            {
            }
        }

        public class UpdateMonoRunner<T, TFlowModifier> : BaseRunner<T> where T : ISveltoTask
                                                                        where TFlowModifier : IRunningTasksInfo
        {
            public UpdateMonoRunner(string name, TFlowModifier modifier) : base(name)
            {
                modifier.runnerName = name;

                _processEnumerator =
                    new CoroutineRunner<T>.Process<TFlowModifier, PlatformProfiler>
                        (_newTaskRoutines, _coroutines, _flushingOperation, modifier);

                UnityCoroutineRunner.StartUpdateCoroutine(_processEnumerator);
            }
        }
    }
}
#endif
