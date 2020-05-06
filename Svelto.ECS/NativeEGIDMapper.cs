using System;
using System.Runtime.CompilerServices;
using Svelto.DataStructures;
using Svelto.DataStructures.Internal;

namespace Svelto.ECS
{
    public readonly struct NativeEGIDMapper<T>:IDisposable where T : unmanaged, IEntityComponent
    {
        readonly NativeFasterDictionary<uint, T> map;
        public ExclusiveGroupStruct groupID { get; }

        public NativeEGIDMapper(ExclusiveGroupStruct groupStructId, NativeFasterDictionary<uint, T> toNative):this()
        {
            groupID = groupStructId;
            map = toNative;
        }

        public uint Count => map.count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Entity(uint entityID)
        {
            unsafe
            {
#if DEBUG && !PROFILE_SVELTO
                if (map.TryFindIndex(entityID, out var findIndex) == false)
                    throw new Exception("Entity not found in this group ".FastConcat(typeof(T).ToString()));
#else
                map.TryFindIndex(entityID, out var findIndex);
#endif
                return ref map.unsafeValues[(int) findIndex];
            }
        }
        
        public bool TryGetEntity(uint entityID, out T value)
        {
            if (map.TryFindIndex(entityID, out var index))
            {
                value = map.GetDirectValue(index);
                return true;
            }

            value = default;
            return false;
        }
        
        public unsafe NB<T>GetArrayAndEntityIndex(uint entityID, out uint index)
        {
            if (map.TryFindIndex(entityID, out index))
            {
                return new NB<T>((IntPtr) map.unsafeValues, map.count, map.capacity);
            }

            throw new ECSException("Entity not found");
        }
        
        public unsafe bool TryGetArrayAndEntityIndex(uint entityID, out uint index, out NB<T> array)
        {
            if (map.TryFindIndex(entityID, out index))
            {
                array =  new NB<T>((IntPtr) map.unsafeValues, map.count, map.capacity);
                return true;
            }

            array = default;
            return false;
        }

        public void Dispose()
        {
            map.Dispose();
        }

        public bool Exists(uint idEntityId)
        {
            return map.count > 0 && map.TryFindIndex(idEntityId, out _);
        }
    }
}