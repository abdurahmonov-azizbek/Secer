using Secer.Models;

namespace Secer.Services;

public interface IRequestService
{
    ValueTask<bool> IsValid(RequestModel request, int seconds, CancellationToken cancellationToken = default);
}
