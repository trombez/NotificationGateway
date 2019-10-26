using NG.BLL.ApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NG.BLL.ApiClient
{
    public class IFTTTClient : IIFTTTClient
    {
        private readonly HttpClient httpClient;

        public IFTTTClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> MakeCall(string address)
        {
            var result = await httpClient.GetStringAsync(address).ConfigureAwait(false);
            return result;
        }
    }
}
