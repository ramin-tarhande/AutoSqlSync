namespace TestDataGen
{
    class MyProgress
    {
        private long count;

        public long Writes
        {
            get
            {
                return count;
            }
        }

        public void AddDone()
        {
            count++;
        }

    }
}
