using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Shared.Tools
{
    public class RwLockHashSet<T> : HashSet<T>
    {
        private int counter;

        public RwLockHashSet()
        {
        }

        public RwLockHashSet(int capacity) : base(capacity)
        {
        }

        public RwLockHashSet(IEqualityComparer<T> comparer) : base(comparer)
        {
        }

        public RwLockHashSet(int capacity, IEqualityComparer<T> comparer) : base(capacity, comparer)
        {
        }

        public RwLockHashSet(IEnumerable<T> set) : base(set)
        {
        }

        public RwLockHashSet(IEnumerable<T> set, IEqualityComparer<T> comparer) : base(set, comparer)
        {
        }

        protected RwLockHashSet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BeginReading()
        {
            RwLock.AcquireForReading(ref counter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FinishReading()
        {
            RwLock.ReleaseAfterReading(ref counter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BeginWriting()
        {
            RwLock.AcquireForWriting(ref counter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FinishWriting()
        {
            RwLock.ReleaseAfterWriting(ref counter);
        }
    }
}