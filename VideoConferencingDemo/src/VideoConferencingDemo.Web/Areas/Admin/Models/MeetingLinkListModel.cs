using Autofac;
using AutoMapper;
using VideoConferencingDemo.Infrastructure.Models;
using VideoConferencingDemo.Infrastructure.Services;
using VideoConferencingDemo.Web.Models;

namespace VideoConferencingDemo.Web.Areas.Admin.Models
{
    public class MeetingLinkListModel : BaseModel
    {
        private IMeetingLinksService _meetingLinksService;

        public MeetingLinkListModel() : base()
        {
        }

        public MeetingLinkListModel(IMeetingLinksService meetingLinksService)
        {
            _meetingLinksService = meetingLinksService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _meetingLinksService = _scope.Resolve<IMeetingLinksService>();
        }

        internal object GetPagedMeetingLinks(DataTablesAjaxRequestModel model)
        {
            var data = _meetingLinksService.GetMeetingLinks
                (
                    model.PageIndex,
                    model.PageSize,
                    model.SearchText,
                    model.GetSortText(new string[] { "UserEmail" })
                );

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.UserEmail,
                            record.MeetingId.ToString(),
                            record.LastUsed.ToString(),
                            record.Id.ToString()
                        }).ToArray()
            };
        }

        public async Task DeleteMeetingLinkAsync(Guid id)
        {
            await _meetingLinksService.DeleteMeetingLinkAsync(id);
        }
    }
}
