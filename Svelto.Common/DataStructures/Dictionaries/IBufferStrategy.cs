using System;

namespace Svelto.DataStructures
{
    public interface IBufferStrategy<T>: IDisposable
    {
        uint capacity { get; }
        
        void Alloc(uint size);
        void Resize(uint size);
        void Clear();
        
        ref T this[uint index] { get ; }
        ref T this[int index] { get ; }
        
        IntPtr ToNativeArray();
        IBuffer<T> ToBuffer();
    }
}