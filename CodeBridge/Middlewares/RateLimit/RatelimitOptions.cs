namespace CodeBridge.Middlewares.RateLimit
{
    public class RatelimitOptions
    {
        public int MaxRequests { get; set; }
        public TimeSpan Interval { get; set; }
    }
}
