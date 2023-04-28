﻿using AutoMapper;
using System.Security.Claims;
using VideoConferencingDemo.Infrastructure.Adapter;
using VideoConferencingDemo.Infrastructure.Entities;
using VideoConferencingDemo.Infrastructure.Exceptions;
using VideoConferencingDemo.Infrastructure.UnitOfWorks;

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

        if (currentUser.TotalGeneratedLinq == null || currentUser.TotalGeneratedLinq < 20)
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
}
