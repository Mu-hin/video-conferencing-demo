using Autofac;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Text.Encodings.Web;
using VideoConferencingDemo.Infrastructure.Exceptions;
using VideoConferencingDemo.Web.Models;

namespace VideoConferencingDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

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
                var meetingId = await model.StoreNewMeetingInformationAsync(User);


                var url = Url.Action(nameof(StartMeeting), "Home",
                            values: new { id = meetingId },
                            protocol: Request.Scheme)!;

                return HtmlEncoder.Default.Encode(url);
            }
            catch(MaxLimitException ex)
            {
                _logger.LogInformation("An user tried to generate meeting link more than max limit", DateTime.UtcNow);
                //return HttpStatusCode.MethodNotAllowed.ToString();
                return "You had reached the max limit of link generation";
            }
            
        }

        public IActionResult StartMeeting(Guid id)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}