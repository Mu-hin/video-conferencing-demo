using Autofac;
using AutoMapper;
using System.Security.Claims;
using VideoConferencingDemo.Infrastructure.Adapter;
using VideoConferencingDemo.Infrastructure.Models;
using VideoConferencingDemo.Infrastructure.Services;

namespace VideoConferencingDemo.Web.Models
{
    public class HomePageModel : BaseModel
    {
        private ISignInManager _signInManager;
        private IMeetingLinksService _meetingLinksService;
        public bool SignedIn { get; set; }

        public HomePageModel() : base()
        {
        }

        public HomePageModel(ISignInManager signInManager, IMeetingLinksService meetingLinksService)
        {
            _signInManager = signInManager;
            _meetingLinksService = meetingLinksService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _signInManager = _scope.Resolve<ISignInManager>();
            _meetingLinksService = _scope.Resolve<IMeetingLinksService>();
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return _signInManager.IsSignedIn(claimsPrincipal);
        }

        public async Task<Guid> CreateMeetingLinkAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await _meetingLinksService.CreateMeetingLinkAsync(claimsPrincipal);
        }
    }
}
