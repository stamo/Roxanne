using Newtonsoft.Json;

namespace Roxanne.Models
{
    public class RelayControlParams
    {
        [JsonProperty("channel")]
        public int Channel { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
