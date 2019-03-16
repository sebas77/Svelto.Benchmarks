﻿using System;
using System.Collections.Generic;
using Svelto.DataStructures.Experimental;
using Svelto.ECS.Internal;

namespace Svelto.ECS
{
    public partial class EnginesRoot
    {
        class DoubleBufferedEntitiesToAdd<T> where T : FasterDictionary<uint, Dictionary<Type, ITypeSafeDictionary>>, new()
        {
            readonly T _entityViewsToAddBufferA = new T();
            readonly T _entityViewsToAddBufferB = new T();

            internal DoubleBufferedEntitiesToAdd()
            {
                other = _entityViewsToAddBufferA;
                current = _entityViewsToAddBufferB;
            }

            internal T other;
            internal T current;
            
            internal void Swap()
            {
                var toSwap = other;
                other = current;
                current = toSwap;
            }

            /// <summary>
            /// Careful, I don't clear the whole group, because otherwise it would be recreated every time
            /// but if a group is removed from good, this should be updated it too
            /// </summary>
            public void ClearOther()
            {
                foreach (var item in other)
                {
                    foreach (var subitem in item.Value)
                    {
                        subitem.Value.Clear();
                    }
                }
            }
        }
    }
}