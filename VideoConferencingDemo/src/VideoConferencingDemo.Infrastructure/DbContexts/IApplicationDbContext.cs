using Microsoft.EntityFrameworkCore;
using VideoConferencingDemo.Infrastructure.Entities;

namespace VideoConferencingDemo.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<MeetingLink> MeetingLinks { get; set; }
    }
}