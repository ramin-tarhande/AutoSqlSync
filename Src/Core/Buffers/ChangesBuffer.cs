using System.Collections.Generic;
using AutoSqlSync.Core.Changes;
using log4net;

namespace AutoSqlSync.Core.Buffers
{
    class ChangesBuffer
    {
        private readonly BufferEvents events;
        private readonly Queue<Change> queue;
        private readonly object syncObject;
        private readonly CoreSettings settings;
        public ChangesBuffer(CoreSettings settings, ILog log)
        {
            syncObject = new object();
            this.settings = settings;
            queue = new Queue<Change>(settings.BufferSizeThreshold);
            events=new BufferEvents(IsEmpty,IsFull, log);
        }

        bool IsEmpty()
        {
            return queue.Count == 0;    
        }

        internal bool IsFull()
        {
            return queue.Count >= settings.BufferSizeThreshold;
        }

        public int Size()
        {
            return queue.Count;
        }

        public StopState WaitForRoom()
        {
            return events.WaitForRoom();
        }

        public StopState WaitForData()
        {
            return events.WaitForData();
        }

        internal void Put(IEnumerable<Change> changes)
        {
            lock (syncObject)
            {
                foreach (var change in changes)
                {
                    queue.Enqueue(change);
                }

                events.PutDone(changes);
            }
        }

        internal Change Take()
        {
            lock (syncObject)
            {
                if (IsEmpty())
                {
                    return null;
                }
                
                var r=queue.Dequeue();

                events.TakeDone();
                
                return r;
            }
        }

        internal void Stop()
        {
            events.Stop();
        }
    }
}
