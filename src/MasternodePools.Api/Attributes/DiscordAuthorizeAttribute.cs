using MasternodePools.Api.Models;
using MasternodePools.Api.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web;

namespace MasternodePools.Api.Attributes
{
    public class DiscordAuthorizeAttribute :  Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken);

            if (string.IsNullOrEmpty(authToken))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var decodedToken = HttpUtility.UrlDecode(authToken);
                var authorization = JsonConvert.DeserializeObject<Authorization>(decodedToken);

                var discordUser = GetUserFromAuthorization(context, authorization);

                context.HttpContext.Items.Add("User", discordUser);
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private DiscordUser GetUserFromAuthorization(AuthorizationFilterContext context, Authorization authorization)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            var authenticationService = (IDiscordAuthenticationService)serviceProvider
                .GetService(typeof(IDiscordAuthenticationService));

            var discordUser = authenticationService.GetUserAsync(authorization).Result;
            return discordUser;
        }
    }
}
