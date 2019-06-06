using MasternodePoolsApi.Attributes;
using MasternodePoolsApi.Services;
using MasternodePoolsApi.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MasternodePoolsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IDiscordAuthenticationService _discordAuthenticationService;

        public AuthenticationController(
            IDiscordAuthenticationService discordAuthenticationService)
        {
            _discordAuthenticationService = discordAuthenticationService;
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            var token = await _discordAuthenticationService.AuthenticateAsync(code);
            if (token == null)
            {
                return Unauthorized();
            }

            Response.Cookies.Append("auth", JsonConvert.SerializeObject(token));

            var user = await _discordAuthenticationService.GetUser(token);

            return Ok();
        }

        [HttpGet("me")]
        [DiscordAuthorize]
        public string Me()
        {
            return "Authorized!";
        }
    }

}
