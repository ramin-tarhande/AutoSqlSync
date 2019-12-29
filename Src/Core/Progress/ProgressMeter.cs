using AutoSqlSync.Core.Buffers;
using AutoSqlSync.Core.Tools;

namespace AutoSqlSync.Core.Progress
{
    class ProgressMeter : SyncProgress
    {
        private readonly Counter readsCounter, writesCounter, failuresCounter, invalidsCounter;
        private readonly ChangesBuffer buffer;
        //private readonly ILog log;
        public ProgressMeter(ChangesBuffer buffer, CoreSettings settings)
        {
            //this.log = log;
            this.buffer = buffer;

            readsCounter=Counter.CreateWrapping(settings.CountWrapping);
            writesCounter = Counter.CreateWrapping(settings.CountWrapping);
            failuresCounter=Counter.CreateSimple();
            invalidsCounter = Counter.CreateSimple();

            readsCounter.Wrapped +=()=>
            {
                failuresCounter.Reset();
                invalidsCounter.Reset();
            };

            ReadState = ReadState.Unknown;
            WriteState = WriteState.Unknown;
            //Reset();
        }

        public int BufferSize
        {
            get
            {
                return buffer.Size();
            }
        }

        public ReadState ReadState { get; set; }
        public WriteState WriteState { get; set; }

        public int Reads
        {
            get
            {
                return (int)readsCounter.Value;
            }
        }

        public int Writes
        {
            get
            {
                return (int)writesCounter.Value;
            }
        }

        public int Failures
        {
            get
            {
                return (int)failuresCounter.Value;
            }
        }

        public int Invalids
        {
            get
            {
                return (int)invalidsCounter.Value;
            }
        }

        public void ReadDone(int count)
        {
            if (count==0)
            {
                return;
            }

            readsCounter.Add(count);
        }

        public void WriteDone()
        {
            writesCounter.AddOne();
        }

        public void InvalidData()
        {
            invalidsCounter.AddOne();
        }

        public void Failed()
        {
            failuresCounter.AddOne();
            //log.InfoFormat("Failures: {0}",failuresCounter.Value);
        }
    }
}
