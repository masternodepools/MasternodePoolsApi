using MasternodePools.Api.Attributes;
using MasternodePools.Api.Models;
using MasternodePools.Api.Services.Abstraction;
using MasternodePools.Data.Entities;
using MasternodePools.Data.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MasternodePools.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private string _websiteUrl;
        private IDiscordAuthenticationService _discordAuthenticationService;
        private IEntityService<User> _userService;

        public AuthenticationController(
            IOptions<AppSettings> appSettings,
            IDiscordAuthenticationService discordAuthenticationService,
            IEntityService<User> userService)
        {
            _discordAuthenticationService = discordAuthenticationService;
            _userService = userService;
            _websiteUrl = appSettings.Value.WebsiteUrl;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            var token = await _discordAuthenticationService.AuthenticateAsync(code);
            if (token == null)
            {
                return Unauthorized();
            }

            var discordUser = await _discordAuthenticationService.GetUserAsync(token);
            var user = await _userService.GetAsync(discordUser.Id);

            Response.Cookies.Append("auth", JsonConvert.SerializeObject(token), new CookieOptions
            {
                Domain = ".mnpools.eu",
            });

            if (user == null)
            {
                await CreateDiscordUserAsync(discordUser);
            }

            return Redirect(_websiteUrl);
        }

        [HttpGet("me")]
        [DiscordAuthorize]
        public string Me()
        {
            var currentUser = HttpContext.Items["User"];
            return JsonConvert.SerializeObject(currentUser);
        }

        private async Task CreateDiscordUserAsync(DiscordUser user)
        {
            await _userService.CreateAsync(new User
            {
                Id = user.Id,
                Discriminator = user.Discriminator,
                Locale = user.Locale,
                UserName = user.UserName
            });
        }
    }

}
