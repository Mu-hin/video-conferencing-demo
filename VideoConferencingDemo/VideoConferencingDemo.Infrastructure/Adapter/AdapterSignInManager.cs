using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ApplicationUserBO = VideoConferencingDemo.Infrastructure.BusinessObjects.ApplicationUser;
using ApplicationUserEO = VideoConferencingDemo.Infrastructure.Entities.Identity.ApplicationUser;
using SignInBO = VideoConferencingDemo.Infrastructure.BusinessObjects.SignIn;
using SignInEO = VideoConferencingDemo.Infrastructure.Entities.SignIn;

namespace VideoConferencingDemo.Infrastructure.Adapter
{
    public class AdapterSignInManager : ISignInManager
    {
        private readonly SignInManager<ApplicationUserEO> _signInManager;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public AdapterSignInManager(SignInManager<ApplicationUserEO> signInManager, IMapper mapper,
            IUserManager userManager)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInBO signInBO)
        {
            var signInEO = _mapper.Map<SignInEO>(signInBO);

            return await _signInManager.PasswordSignInAsync(signInEO.Email, signInEO.Password,
                signInEO.RememberMe, lockoutOnFailure: false);
        }

        public async Task RefreshSignInAsync(ClaimsPrincipal claimsPrincipal)
        {
            var userBO = await _userManager.GetUserAsync(claimsPrincipal);
            var userEO = _mapper.Map<ApplicationUserEO>(userBO);

            await _signInManager.RefreshSignInAsync(userEO);
        }

        public async Task SignInAsync(ApplicationUserBO userBO)
        {
            var userEO = _mapper.Map<ApplicationUserEO>(userBO);
            await _signInManager.SignInAsync(userEO, isPersistent: false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public  bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return _signInManager.IsSignedIn(claimsPrincipal);
        }
    }
}
