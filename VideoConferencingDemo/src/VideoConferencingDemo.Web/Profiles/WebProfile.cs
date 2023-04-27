using AutoMapper;
using VideoConferencingDemo.Infrastructure.BusinessObjects;
using VideoConferencingDemo.Web.Models;
using ApplicationUserBO = VideoConferencingDemo.Infrastructure.BusinessObjects.ApplicationUser;
using ApplicationUserEO = VideoConferencingDemo.Infrastructure.Entities.Identity.ApplicationUser;

namespace VideoConferencingDemo.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<SignUpModel, ApplicationUserEO>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email))
                .ReverseMap();

            CreateMap<SignUpModel, ApplicationUserBO>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.Email))
                .ReverseMap();

            CreateMap<SignInModel, SignIn>().ReverseMap();

            CreateMap<ApplicationUserEO, ApplicationUserBO>()
                .ReverseMap();
        }
    }
}
