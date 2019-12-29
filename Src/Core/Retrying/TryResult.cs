namespace AutoSqlSync.Core.Retrying
{
    struct TryResult
    {
        public bool Success { get; private set; }
        public Retry Retry { get; private set; }
        public TryResult(bool success, Retry retry)
            : this()
        {
            Success = success;
            Retry = retry;
        }

        public bool ShouldRetry()
        {
            return this.Retry == Retry.Yes;
        }
    }
}
