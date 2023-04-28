using Microsoft.EntityFrameworkCore;
using VideoConferencingDemo.Infrastructure.DbContexts;
using VideoConferencingDemo.Infrastructure.Entities;

namespace VideoConferencingDemo.Infrastructure.Repositories;

public class MeetingLinkRepository : Repository<MeetingLink, Guid>, IMeetingLinkRepository
{
    public MeetingLinkRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }
}
