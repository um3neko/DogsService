using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace CodeBridge.Middlewares.RateLimit;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _maxRequests;
    private readonly TimeSpan _interval;
    private readonly ConcurrentDictionary<string, RateLimitData> _rateLimitDictionary;
    private readonly SemaphoreSlim _lock;

    public RateLimitMiddleware(RequestDelegate next, IOptions<RatelimitOptions> options)
    {
        _next = next;

        _maxRequests = options.Value.MaxRequests;
        _interval = options.Value.Interval;

        _rateLimitDictionary = new ConcurrentDictionary<string, RateLimitData>();
        _lock = new SemaphoreSlim(1, 1);
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var ipAddress = context.Connection.RemoteIpAddress.ToString();
        var rateLimitData = _rateLimitDictionary.GetOrAdd(ipAddress, new RateLimitData());

        await _lock.WaitAsync();

        try
        {
            var currentTime = DateTime.UtcNow;
            if (currentTime - rateLimitData.LastRequestTime > _interval)
            {
                rateLimitData.RequestCount = 0;
                rateLimitData.LastRequestTime = currentTime;
            }

            if (rateLimitData.RequestCount >= _maxRequests)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too Many Requests.");
                return;
            }

            rateLimitData.RequestCount++;
            rateLimitData.LastRequestTime = currentTime;
        }
        finally
        {
            _lock.Release();
        }

        await _next(context);
    }
}
