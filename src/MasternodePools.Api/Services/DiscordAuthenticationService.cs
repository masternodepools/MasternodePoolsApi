using MasternodePools.Api.Models;
using MasternodePools.Api.Services.Abstraction;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MasternodePools.Api.Services
{
    public class DiscordAuthenticationService : IDiscordAuthenticationService
    {
        private readonly string _apiEndpoint = "https://discordapp.com/api/v6";
        private readonly string _clientId;
        private readonly string _clientSecret;

        public DiscordAuthenticationService(IOptions<AppSettings> appSettings)
        {
            _clientId = appSettings.Value.DiscordClientId;
            _clientSecret = appSettings.Value.DiscordSecret;
        }

        public async Task<DiscordUser> GetUserAsync(Models.Authorization token)
        {
            using (var client = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Get, $"{_apiEndpoint}/users/@me");
                req.Headers.Add("authorization", $"{token.TokenType} {token.AccessToken}");

                var res = await client.SendAsync(req);
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                var contentString = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DiscordUser>(contentString);
            }
        }

        public async Task<Models.Authorization> AuthenticateAsync(string code)
        {
            using (var client = new HttpClient())
            {
                var content = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", "https://localhost:44345/api/authentication"),
                    new KeyValuePair<string, string>("scope", "identify")
                };

                var req = new HttpRequestMessage(HttpMethod.Post, $"{_apiEndpoint}/oauth2/token")
                {
                    Content = new FormUrlEncodedContent(content)
                };

                var res = await client.SendAsync(req);
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                var contentString = await res.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Models.Authorization>(contentString);
                return token;
            }
        }
    }
}
