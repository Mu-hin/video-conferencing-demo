using Autofac;
using AutoMapper;
using System.Security.Claims;
using VideoConferencingDemo.Infrastructure.Adapter;
using VideoConferencingDemo.Infrastructure.Models;

namespace VideoConferencingDemo.Web.Models
{
    public class HomePageModel : BaseModel
    {
        private ISignInManager _signInManager;
        public bool SignedIn { get; set; }

        public HomePageModel() : base()
        {
        }

        public HomePageModel(ISignInManager signInManager)
        {
            _signInManager = signInManager;
        }


        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _signInManager = _scope.Resolve<ISignInManager>();
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            //SignedIn = _signInManager.IsSignedIn(claimsPrincipal);
            return _signInManager.IsSignedIn(claimsPrincipal);
        }

        public string GenerateNewMeetingLink()
        {
            return "";
        }
    }
}
