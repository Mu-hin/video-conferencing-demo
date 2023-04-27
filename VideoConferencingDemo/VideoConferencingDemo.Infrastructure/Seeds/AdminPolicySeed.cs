using VideoConferencingDemo.Infrastructure.Entities.Identity;

namespace VideoConferencingDemo.Infrastructure.Seeds
{
    public class AdminPolicySeed
    {
        internal ApplicationUserClaim[] AdminClaims
        {
            get
            {
                return new ApplicationUserClaim[]
                {
                    new ApplicationUserClaim{Id = 1, UserId = Guid.Parse("95E139BB-6751-4D4B-B14F-12E1597EF982"), ClaimType = "LinkManagement", ClaimValue = "true"}
                };
            }
        }
    }
}
