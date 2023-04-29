using Microsoft.AspNetCore.Authorization;

namespace VideoConferencingDemo.Infrastructure.Securities;

public class AdminPolicyHandler : AuthorizationHandler<AdminPolicy>
{
    protected override Task HandleRequirementAsync(
           AuthorizationHandlerContext context,
           AdminPolicy requirement)
    {
        if (context.User.HasClaim(x => x.Type == "LinkManagement" && x.Value == "true"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
