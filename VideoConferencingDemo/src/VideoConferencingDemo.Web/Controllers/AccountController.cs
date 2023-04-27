using Autofac;
using AutoMapper;
using VideoConferencingDemo.Infrastructure.BusinessObjects;
using VideoConferencingDemo.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VideoConferencingDemo.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IMapper mapper,
            ILifetimeScope scope,
            ILogger<AccountController> logger)
        {
            _scope = scope;
            _logger = logger;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignUp(string? returnUrl = null)
        {
            var model = _scope.Resolve<SignUpModel>();

            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                ApplicationUser user = _mapper.Map<ApplicationUser>(model);

                model.ResolveDependency(_scope);
                var result = await model.CreateUserAsync();

                if (result.Succeeded)
                {
                    await model.AddAdminClaimsAsync();
                    //await model.AddUserClaimsAsync();

                    if (model.RequireConfirmedAccount())
                    {
                        //return RedirectToAction(nameof(RegisterConfirmation),
                        //    new RegisterConfirmationModel { Email = model.Email });
                    }
                    else
                    {
                        var signInModel = new SignInModel();
                        signInModel.ResolveDependency(_scope);

                        await signInModel.SignInUserAsync(user.Email!);

                        return LocalRedirect(model.ReturnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string? projectId, string? returnUrl = null,
            string? userEmail = null)
        {
            var model = _scope.Resolve<SignInModel>();

            model.ReturnUrl = returnUrl;
            model.UserEmail = userEmail;
            model.ReturnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                var result = await model.SignInWithPasswordAsync();

                if (result.Succeeded)
                {
                    return LocalRedirect(model.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> SignOut(string? returnUrl = null)
        {
            var model = _scope.Resolve<SignInModel>();

            await model.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction();
            }
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
