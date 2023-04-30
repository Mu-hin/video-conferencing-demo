using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VideoConferencingDemo.Infrastructure.BusinessObjects;

namespace VideoConferencingDemo.Infrastructure.Adapter
{
    public interface IUserManager
    {
        public Task<(ApplicationUser user, IdentityResult result)> CreateAsync(ApplicationUser user,
            string password);
        public Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        public Task<ApplicationUser> GetUserByEmailAsync(string email);
        public Task<IdentityResult> ConfirmEmailAsync(string email, string code);
        public Task AddClaimsAsync(string email, Claim[] claims);
        public bool RequireConfirmedAccount();
        public Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        public Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, 
            string oldPassword, string newPassword);

        public Task<IdentityResult> UploadImageAsync(ClaimsPrincipal claimsPrincipal, string image);
        public Task<bool> GetAccountConfirmedStatusAsync(string email);
        public Task<Guid> GetUserIdAsync(ClaimsPrincipal claimsPrincipal);
        public Task<bool> CheckCurrentUserAsync(ClaimsPrincipal claimsPrincipal, string email);
        public Task AddToRolesAsync(string email, IList<string> claims);
        public Task IncreaseTotalLinkInfoAsync(ClaimsPrincipal claimsPrincipal);
        public Task DecreaseTotalLinkInfoAsync(string email);
    }
}
