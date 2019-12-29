using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestDataGen.Data
{
    class Generator
    {
        private int maxId;
        private readonly GenDa da;
        private readonly MyProgress progress;
        public Generator(GenDa da, int maxId, MyProgress progress)
        {
            this.progress = progress;
            this.da = da;
            this.maxId = maxId;
        }

        public async Task Insert(int count)
        {
            var up = count;

            try
            {
                for (var i = 0; i < up; i++)
                {
                    await InsertSingle();
                }
            }
            catch(Exception)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();    
                }
            }
        }

        async Task InsertSingle()
        {
            maxId++;

            var x = maxId;

            var r = CreateRecord(x);
            await da.Insert(r);
            

            this.progress.AddDone();
        }

        static ASourceRecord CreateRecord(int x)
        {
            var r = new ASourceRecord
            {
                Id = x,
                Title = "t " + x,
                Description = "d " + x
            };

            return r;
        }
    }
}
