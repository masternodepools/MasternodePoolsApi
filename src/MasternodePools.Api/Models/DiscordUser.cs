using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasternodePools.Api.Models
{
    public class DiscordUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("discriminator")]
        public string Discriminator { get; set; }

        [JsonProperty("nametag")]
        public string NameTag
        {
            get
            {
                return $"{UserName}#{Discriminator}";
            }
        }
    }
}
