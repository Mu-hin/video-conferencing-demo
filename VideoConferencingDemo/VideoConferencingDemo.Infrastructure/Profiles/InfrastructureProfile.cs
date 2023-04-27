using AutoMapper;
using SignInEO = VideoConferencingDemo.Infrastructure.Entities.SignIn;
using SignInBO = VideoConferencingDemo.Infrastructure.BusinessObjects.SignIn;
using ApplicationUserBO = VideoConferencingDemo.Infrastructure.BusinessObjects.ApplicationUser;
using ApplicationUserEO = VideoConferencingDemo.Infrastructure.Entities.Identity.ApplicationUser;

namespace VideoConferencingDemo.Infrastructure.Profiles
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<SignInBO, SignInEO>()
                .ReverseMap();

            CreateMap<ApplicationUserEO, ApplicationUserBO>()
                  .ReverseMap();

            CreateMap<ApplicationUserEO, ApplicationUserBO>()
                  .ReverseMap();
        }
    }
}
