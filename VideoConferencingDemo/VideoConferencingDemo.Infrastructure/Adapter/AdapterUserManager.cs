using AutoMapper;
using KeyGenerator.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using ApplicationUserBO = KeyGenerator.Infrastructure.BusinessObjects.ApplicationUser;
using ApplicationUserEO = KeyGenerator.Infrastructure.Entities.Identity.ApplicationUser;

namespace VideoConferencingDemo.Infrastructure.Adapter
{
    public class AdapterUserManager : IUserManager
    {
        private readonly UserManager<ApplicationUserEO> _userManager;
        private readonly IMapper _mapper;

        public AdapterUserManager(UserManager<ApplicationUserEO> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        private async Task<ApplicationUserEO> FindUserByEmailAsync(string email)
        {
            if (email == null)
                throw new InvalidOperationException($"Email can't be null");

            var userEO = await _userManager.FindByEmailAsync(email);

            if (userEO == null)
                throw new UserNotFoundException($"There is no user with this email addess {email}");

            return userEO;
        }

        private async Task<ApplicationUserEO> GetUserByClaimPrincipalAsync(ClaimsPrincipal claimsPrincipal)
        {
            var userEO = await _userManager.GetUserAsync(claimsPrincipal);

            if (userEO == null)
                throw new UserNotFoundException($"Unable to load user with this claim principal: {claimsPrincipal}");

            return userEO;
        }

        public async Task<ApplicationUserBO> GetUserByEmailAsync(string email)
        {
            var userEO = await FindUserByEmailAsync(email);
            var userBO = _mapper.Map<ApplicationUserBO>(userEO);

            return userBO;
        }

        public async Task AddClaimsAsync(string email, Claim[] claims)
        {
            var user = await FindUserByEmailAsync(email);
            await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task AddToRolesAsync(string email, IList<string> claims)
        {
            var user = await FindUserByEmailAsync(email);
            await _userManager.AddToRolesAsync(user, claims);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string email, string code)
        {
            var user = await FindUserByEmailAsync(email);
            if (await _userManager.IsEmailConfirmedAsync(user))
                return new IdentityResult();

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result;
        }

        public async Task<(ApplicationUserBO, IdentityResult)> CreateAsync(ApplicationUserBO user,
            string password)
        {
            var applicationUserEO = _mapper.Map<ApplicationUserEO>(user);
            var result = await _userManager.CreateAsync(applicationUserEO, password);
            var applicationUserBO = _mapper.Map<ApplicationUserBO>(applicationUserEO);

            return (applicationUserBO, result);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUserBO user)
        {
            var applicationUserEO = _mapper.Map<ApplicationUserEO>(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUserEO);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return code;
        }

        public bool RequireConfirmedAccount()
        {
            return _userManager.Options.SignIn.RequireConfirmedAccount;
        }

        public async Task<ApplicationUserBO> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var userEO = await GetUserByClaimPrincipalAsync(claimsPrincipal);
            var userBO = _mapper.Map<ApplicationUserBO>(userEO);

            return userBO;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal claimsPrincipal,
            string oldPassword, string newPassword)
        {
            var user = await GetUserByClaimPrincipalAsync(claimsPrincipal);
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> UploadImageAsync(ClaimsPrincipal claimsPrincipal, string image)
        {
            var userEO = await GetUserByClaimPrincipalAsync(claimsPrincipal);
            userEO.Image = image;
            return await _userManager.UpdateAsync(userEO);
        }

        public async Task<bool> GetAccountConfirmedStatusAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);

            if (user.EmailConfirmed)
                return true;
            else
                throw new InvalidOperationException("you have to confimr you account before joining" +
                    " any project as a worker");
        }

        public async Task<Guid> GetUserIdAsync(ClaimsPrincipal claimsPrincipal)
        {
            var userEO = await GetUserByClaimPrincipalAsync(claimsPrincipal);
            return userEO.Id;
        }

        public async Task<bool> CheckCurrentUserAsync(ClaimsPrincipal claimsPrincipal, string email)
        {
            var userEO = await GetUserByClaimPrincipalAsync(claimsPrincipal);

            if (userEO.Email == email)
                return true;
            else
                return false;
        }
    }
}
