using Newtonsoft.Json;
using Roxanne.Contracts;
using Roxanne.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Roxanne.Services
{
    public class OmegaRequester : IOmegaRequester
    {
        private const string ApiKey = "your-api-key";

        private const string Url = "https://api.onion.io/v1/devices/id-of-the-device/i2c_exp/relay-exp";

        public bool SetBulbState(int state)
        {
            bool result = false;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Url);
                var jsonData = JsonConvert.SerializeObject(new RelayControlDataModel(0, state));

                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);
                client.Timeout = TimeSpan.FromSeconds(3);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

                try
                {
                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string content = response.Content.ReadAsStringAsync().Result;

                        result = content != null && content.ToLower().Contains("success");
                    }
                }
                catch (Exception)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
