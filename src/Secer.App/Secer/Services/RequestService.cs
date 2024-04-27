using Microsoft.Extensions.Caching.Distributed;
using Secer.Models;
using System.Text.Json;

namespace Secer.Services;

public class RequestService(IDistributedCache distributedCache) : IRequestService
{
    public async ValueTask<bool> IsValid(RequestModel request, int seconds, CancellationToken cancellationToken = default)
    {
        var key = $"{request.MethodName}-{request.IpAddress}";
        var value = await distributedCache.GetStringAsync(key, cancellationToken);

        var requestModel = new RequestModel
        {
            IpAddress = request.IpAddress,
            MethodName = request.MethodName,
            RequestTime = DateTime.UtcNow
        };

        var expiration = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds),
        };

        if (value is null)
        {
            await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(requestModel), expiration, cancellationToken);

            return true;
        }

        await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(requestModel), expiration, cancellationToken);
        return false;
    }
}
