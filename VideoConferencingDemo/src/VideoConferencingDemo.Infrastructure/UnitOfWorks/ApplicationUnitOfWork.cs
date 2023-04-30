using VideoConferencingDemo.Infrastructure.DbContexts;
using VideoConferencingDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace VideoConferencingDemo.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IMeetingLinkRepository MeetingLinks { get; private set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            IMeetingLinkRepository meetingLinks) : base((DbContext)dbContext)
        {
            MeetingLinks = meetingLinks;
        }
    }
}
