using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace SIG.SIGCMS
{
    public class EnterBuildingRequirement : IAuthorizationRequirement
    {
    }
    //http://bubuko.com/infodetail-2371138.html
    public class BadgeEntryHandler : AuthorizationHandler<EnterBuildingRequirement>
    {
        readonly ILogger _logger;
        public BadgeEntryHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
        {
            _logger.LogInformation("Inside my handler");
            //if (context.User.HasClaim(c => c.Type == ClaimTypes.Email &&
            //                               c.Issuer == "http://microsoftsecurity"))
            //{
           
            if (context.User.Identity.IsAuthenticated)
                context.Succeed(requirement);
            //}
            return Task.CompletedTask;
        }
    }

}
