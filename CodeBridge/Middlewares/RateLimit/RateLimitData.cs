namespace CodeBridge.Middlewares.RateLimit;

public class RateLimitData
{
    public int RequestCount { get; set; }
    public DateTime LastRequestTime { get; set; }
}
