using System.Security.Claims;

namespace VideoConferencingDemo.Infrastructure.Services;

public interface IMeetingLinksService
{
    public Task<Guid> StoreMeetingInformationAsync(ClaimsPrincipal claimsPrincipal);
}
