using MasternodePoolsApi.Models;
using MasternodePoolsApi.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace MasternodePoolsApi.Services
{
    public class DiscordAuthorizeAttribute :  Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Cookies.TryGetValue("auth", out string authCookie);
            if (string.IsNullOrEmpty(authCookie))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authorization = JsonConvert.DeserializeObject<Authorization>(authCookie);
            if (string.IsNullOrEmpty(authCookie))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var serviceProvider = context.HttpContext.RequestServices;
            var authenticationService = (IDiscordAuthenticationService)serviceProvider
                .GetService(typeof(IDiscordAuthenticationService));

            var discordUser = authenticationService.GetUser(authorization);
        }
    }
}
