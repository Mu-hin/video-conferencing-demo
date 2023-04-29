using AutoMapper;
using System.Security.Claims;
using VideoConferencingDemo.Infrastructure.Adapter;
using VideoConferencingDemo.Infrastructure.Entities;
using VideoConferencingDemo.Infrastructure.Exceptions;
using VideoConferencingDemo.Infrastructure.UnitOfWorks;
using MeetingLinkBO = VideoConferencingDemo.Infrastructure.BusinessObjects.MeetingLink;
using MeetingLinkEO = VideoConferencingDemo.Infrastructure.Entities.MeetingLink;

namespace VideoConferencingDemo.Infrastructure.Services;

public class MeetingLinksService : IMeetingLinksService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    private readonly IUserManager _userManager;
    private readonly IMapper _mapper;

    public MeetingLinksService(IApplicationUnitOfWork unitOfWork, IUserManager userManager, IMapper mapper)
    {
        _applicationUnitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Guid> StoreMeetingInformationAsync(ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = await _userManager.GetUserAsync(claimsPrincipal);
        var meetingLink = new MeetingLink();

        if (currentUser.TotalGeneratedLinq < 20)
        {
            meetingLink.MeetingId = Guid.NewGuid();
            meetingLink.UserEmail = currentUser.Email!;
            meetingLink.LastUsed = DateTime.UtcNow;

            await _applicationUnitOfWork.MeetingLinks.AddAsync(meetingLink);
            await _applicationUnitOfWork.SaveAsync();
        }
        else
        {
            throw new MaxLimitException("You had reached the max limit of link generation");
        }

        await _userManager.UpdateTotalLinkInfo(claimsPrincipal);
        return meetingLink.MeetingId;
    }

    public async Task<bool> CheckLinkOwner(Guid meetingId, ClaimsPrincipal claimsPrincipal)
    {
        var currentUser = await _userManager.GetUserAsync(claimsPrincipal);
        var meetingLinkInfo = _applicationUnitOfWork.MeetingLinks.Get(x => x.MeetingId == meetingId, "").FirstOrDefault();

        if(meetingLinkInfo != null)
        {
            if (currentUser.Email == meetingLinkInfo.UserEmail)
            {
                meetingLinkInfo.LastUsed = DateTime.UtcNow;
                await _applicationUnitOfWork.SaveAsync();

                return true;
            }
                
            return false;
        }
        else
        {
            throw new InvalidLinkException("An user tried to connect with invalid meeting link");
        }
    }

    public async Task DeleteMeetingLink(Guid id)
    {
        await _applicationUnitOfWork.MeetingLinks.RemoveAsync(id);
        await _applicationUnitOfWork.SaveAsync();
    }

    public (int total, int totalDisplay, IList<MeetingLinkBO> records) GetMeetingLinks(int pageIndex,
            int pageSize, string searchText, string orderby)
    {
        if (orderby == String.Empty)
            orderby = null;

        var results = _applicationUnitOfWork.MeetingLinks.GetDynamic(
                    x => x.UserEmail.Contains(searchText),
                    orderby, "", pageIndex, pageSize, true);

        IList<MeetingLinkBO> keyRequests = new List<MeetingLinkBO>();

        foreach (MeetingLinkEO keyRequestEntity in results.data)
        {
            keyRequests.Add(_mapper.Map<MeetingLinkBO>(keyRequestEntity));
        }

        return (results.total, results.totalDisplay, keyRequests);
    }
}
