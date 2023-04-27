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
    public class SignUpModel : BaseModel
    {
        [Required]
        public string Email { get; set; }
        [Required, StringLength(30, MinimumLength = 6,
            ErrorMessage = "password must be less than 30 character and greater than 5 character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Confirm Password field is required."), Compare(nameof(Password),
                  ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "The Name field is required"),
            StringLength(30, ErrorMessage = "name must be less than 30 character")]
        public string Name { get; set; }
        public string? ReturnUrl { get; set; }

        private IUserManager _userManager;
        private ApplicationUser _user;

        public SignUpModel() : base()
        {
        }

        public SignUpModel(IMapper mapper, IUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _userManager = _scope.Resolve<IUserManager>();
        }

        public async Task<IdentityResult> CreateUserAsync()
        {
            var user = _mapper.Map<ApplicationUser>(this);
            var Result = await _userManager.CreateAsync(user, Password);
            _user = Result.user;

            return Result.result;
        }

        public bool RequireConfirmedAccount()
        {
            return _userManager.RequireConfirmedAccount();
        }

        public async Task AddUserClaimsAsync()
        {
            var claims = new Claim[]
            {
                new Claim("Name", Name),
                new Claim("Email", Email),
                new Claim("GetKey", "true"),
                new Claim("ValidateKey", "true"),
            };

            await _userManager.AddClaimsAsync(Email, claims);
        }

        public async Task AddAdminClaimsAsync()
        {
            var claims = new Claim[]
            {
                new Claim("Name", Name),
                new Claim("Email", Email),
                new Claim("GetKey", "true"),
                new Claim("ValidateKey", "true"),
                new Claim("ApproveKey", "true"),
            };

            await _userManager.AddClaimsAsync(Email, claims);
        }
    }
}
