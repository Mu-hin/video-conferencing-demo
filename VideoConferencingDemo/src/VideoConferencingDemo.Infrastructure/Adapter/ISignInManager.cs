using VideoConferencingDemo.Infrastructure.BusinessObjects;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace VideoConferencingDemo.Infrastructure.Adapter
{
    public interface ISignInManager
    {
        public Task<SignInResult> PasswordSignInAsync(SignIn model);
        public Task SignOutAsync();
        public Task SignInAsync(ApplicationUser user);
        public Task RefreshSignInAsync(ClaimsPrincipal claimsPrincipal);
        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal);
    }
}
