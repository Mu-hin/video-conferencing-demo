using Microsoft.AspNetCore.Mvc;
using VideoConferencingDemo.Web.Models;
using VideoConferencingDemo.Web.Areas.Admin.Models;
using Autofac;
using Microsoft.AspNetCore.Authorization;

namespace VideoConferencingDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminPolicy")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ILifetimeScope _scope;

        public DashboardController(ILogger<DashboardController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllMeetingLink()
        {
            var model = new MeetingLinkListModel();
            model.ResolveDependency(_scope);

            var dataTableModel = new DataTablesAjaxRequestModel(Request);

            return Json(model.GetPagedMeetingLinks(dataTableModel));
        }

        public async Task<IActionResult> DeleteLink(Guid id)
        {
            try
            {
                var model = new MeetingLinkListModel();
                model.ResolveDependency(_scope);

                await model.DeleteMeetingLinkAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occured when trying to delete meeting link: {ex.Message}",
                    ex.StackTrace);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
