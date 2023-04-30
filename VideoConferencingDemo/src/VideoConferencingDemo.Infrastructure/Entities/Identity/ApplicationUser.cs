using Microsoft.AspNetCore.Identity;

namespace VideoConferencingDemo.Infrastructure.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string? Image { get; set; }
        public int TotalGeneratedLinq { get; set; } = 0;
    }
}
