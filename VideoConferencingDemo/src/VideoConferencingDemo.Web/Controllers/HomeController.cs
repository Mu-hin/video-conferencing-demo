﻿using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Encodings.Web;
using VideoConferencingDemo.Infrastructure.Exceptions;
using VideoConferencingDemo.Web.Models;

namespace VideoConferencingDemo.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomePageModel();
            model.ResolveDependency(_scope);
            model.IsSignedIn(User);

            return View(model);
        }

        public async Task<string> GetNewMeetingLink()
        {
            try
            {
                var model = new HomePageModel();
                model.ResolveDependency(_scope);
                var meetingId = await model.CreateMeetingLinkAsync(User);


                var url = Url.Action(nameof(StartMeeting), "Home",
                            values: new { id = meetingId },
                            protocol: Request.Scheme)!;

                return HtmlEncoder.Default.Encode(url);
            }
            catch (MaxLimitException ex)
            {
                _logger.LogInformation("An user tried to generate meeting link more than max limit");
                //return HttpStatusCode.MethodNotAllowed.ToString();
                return ex.Message;
            }

        }

        public async Task<IActionResult> StartMeeting(Guid id)
        {
            var model = new StartMeetingModel();
            model.MeetingId = id;

            try
            {
                model.ResolveDependency(_scope);
                await model.CheckLinkOwnerAsync(id, User);
            }
            catch (InvalidLinkException ex)
            {
                _logger.LogInformation(ex.Message);
                return RedirectToAction(nameof(InvalidLink));
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult InvalidLink()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}