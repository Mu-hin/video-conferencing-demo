using System.Security.Claims;
using VideoConferencingDemo.Infrastructure.BusinessObjects;

namespace VideoConferencingDemo.Infrastructure.Services;

public interface IMeetingLinksService
{
    public Task<Guid> CreateMeetingLinkAsync(ClaimsPrincipal claimsPrincipal);
    public Task<bool> CheckLinkOwnerAsync(Guid meetingId, ClaimsPrincipal claimsPrincipal);
    public Task DeleteMeetingLinkAsync(Guid id);
    public (int total, int totalDisplay, IList<MeetingLink> records) GetMeetingLinks(int pageIndex,
            int pageSize, string searchText, string orderby);
}
