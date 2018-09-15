using Newtonsoft.Json;

namespace Roxanne.Models
{
    public class RelayControlDataModel
    {
        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("params")]
        public RelayControlParams Params { get; set; }

        public RelayControlDataModel(int channel, int state)
        {
            Command = "set";

            Params = new RelayControlParams()
            {
                Address = "0x27",
                Channel = channel,
                State = state
            };
        }
    }
}