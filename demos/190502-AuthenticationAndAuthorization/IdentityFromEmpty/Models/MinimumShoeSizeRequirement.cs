using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IdentityFromEmpty.Models
{
    public class MinimumShoeSizeRequirement : IAuthorizationRequirement
    {
        public MinimumShoeSizeRequirement(int minimumShoeSize)
        {
            MinimumShoeSize = minimumShoeSize;
        }

        public int MinimumShoeSize { get; }
    }

    public class MinimumShoeSizeHandler : AuthorizationHandler<MinimumShoeSizeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumShoeSizeRequirement requirement)
        {
            var sizeClaim = context.User.FindFirst("shoesize");
            if (sizeClaim != null && int.Parse(sizeClaim.Value)>= requirement.MinimumShoeSize)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
