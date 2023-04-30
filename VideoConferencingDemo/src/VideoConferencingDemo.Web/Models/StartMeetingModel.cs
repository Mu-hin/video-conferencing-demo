using Autofac;
using AutoMapper;
using System.Security.Claims;
using VideoConferencingDemo.Infrastructure.Services;
using VideoConferencingDemo.Infrastructure.Models;

namespace VideoConferencingDemo.Web.Models
{
    public class StartMeetingModel : BaseModel
    {
        private IMeetingLinksService _meetingLinksService;
        public bool IsLinkOwner { get; set; }
        public Guid MeetingId { get; set; }

        public StartMeetingModel() : base()
        {
        }

        public StartMeetingModel(IMeetingLinksService meetingLinksService)
        {
            _meetingLinksService = meetingLinksService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _meetingLinksService = _scope.Resolve<IMeetingLinksService>();
        }

        public async Task CheckLinkOwnerAsync(Guid meetingId, ClaimsPrincipal claimsPrincipal)
        {
            IsLinkOwner = await _meetingLinksService.CheckLinkOwnerAsync(meetingId, claimsPrincipal);
        }
    }
}
