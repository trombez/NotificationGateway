using Microsoft.Extensions.Options;
using NG.BLL.ApiClient;
using NG.BLL.Configuration;
using NG.BLL.Constants;
using NG.BLL.Models;
using NG.BLL.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace NG.BLL.Providers
{
    public class IFTTTProvider : ProviderBase, ICallerProvider
    {
        private readonly IFTTTClient iFTTTClient;

        public override string ProviderName { get; } = ProvidersConstants.IFTTT;

        public IFTTTProvider(IOptions<ProvidersSettings> optionsprovider,
            IOptions<AppSettings> optionsApp,
            IFTTTClient iFTTTClient) : base(optionsprovider, optionsApp)
        {
            this.iFTTTClient = iFTTTClient ?? throw new ArgumentNullException(nameof(iFTTTClient));
        }

        public async Task<ModelBase> MakeCall(NotificationModel model)
        {
            var result = new ModelBase();

            var settingsResult = GetSettings();
            if (!settingsResult.IsValid)
                return settingsResult;

            var settings = settingsResult.ProviderSettings;

            try
            {
                var address = $"/trigger/{model.Action}/with/key/{settings.AuthToken}";
                var resultAPI = await iFTTTClient.MakeCall(address).ConfigureAwait(false);
                result.AddInfo(resultAPI);
            }
            catch(Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }
    }
}
