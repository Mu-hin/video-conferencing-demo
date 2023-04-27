using Autofac;
using AutoMapper;
using VideoConferencingDemo.Infrastructure.Adapter;
using VideoConferencingDemo.Infrastructure.BusinessObjects;
using VideoConferencingDemo.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace VideoConferencingDemo.Web.Models
{
    public class SignInModel : BaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(30, MinimumLength = 6,
            ErrorMessage = "password must be less than 30 character and greater than 5 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
        public string? UserEmail { get; set; }

        private ISignInManager _signInManager;
        private IUserManager _userManager;

        public SignInModel() : base()
        {
        }

        public SignInModel(ISignInManager signInManager, IUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _signInManager = _scope.Resolve<ISignInManager>();
            _userManager = _scope.Resolve<IUserManager>();
        }

        public async Task<SignInResult> SignInWithPasswordAsync()
        {
            var signInBO = _mapper.Map<SignIn>(this);
            return await _signInManager.PasswordSignInAsync(signInBO);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task SignInUserAsync(string email)
        {
            var user = await _userManager.GetUserByEmailAsync(email);

            await _signInManager.SignInAsync(user);
        }

        public async Task RefreshSignInAsync(ClaimsPrincipal claimsPrincipal)
        {
            await _signInManager.RefreshSignInAsync(claimsPrincipal);
        }
    }
}
