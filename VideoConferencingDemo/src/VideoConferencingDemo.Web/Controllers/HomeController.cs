using Autofac;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public IActionResult GetNewMeetingLink()
        {
            var model = new HomePageModel();
            model.ResolveDependency(_scope);
            model.IsSignedIn(User);

            return View(model);
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