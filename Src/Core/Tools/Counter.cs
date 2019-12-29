using System;

namespace AutoSqlSync.Core.Tools
{
    public class Counter
    {
        public static Counter CreateWrapping(long max)
        {
            return new Counter(max);
        }

        public static Counter CreateSimple()
        {
            return new Counter(null);
        }

        public long Value { get; private set; }
        public event Action Wrapped;

        private readonly long? max;
        private Counter(long? max)
        {
            this.max = max;
        }

        public void AddOne()
        {
            Add(1);
        }

        public void Add(int x)
        {
            this.Value += x;

            DoWrapping();
        }

        void DoWrapping()
        {
            if (this.max == null)
            {
                return;
            }

            if (this.Value >= this.max.Value)
            {
                this.Value = this.Value % this.max.Value;
                FireWrapped();
            }
        }

        void FireWrapped()
        {
            if (Wrapped != null)
            {
                Wrapped();
            }
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}
