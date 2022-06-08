using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Authorization
{
    public class InitiativeResourceOperationHandler : AuthorizationHandler<ResourceOperationRequirement, Initiative>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Initiative resource)
        {
            if (requirement.OperationType == OperationType.Create || requirement.OperationType == OperationType.Read)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (resource.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
