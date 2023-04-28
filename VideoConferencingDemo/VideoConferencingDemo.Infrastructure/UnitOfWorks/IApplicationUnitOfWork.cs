using VideoConferencingDemo.Infrastructure.Repositories;

namespace VideoConferencingDemo.Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IMeetingLinkRepository MeetingLinks { get; }
    }
}