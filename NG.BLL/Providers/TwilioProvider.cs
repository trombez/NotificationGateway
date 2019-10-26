using Microsoft.Extensions.Options;
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
    public class TwilioProvider : ProviderBase, ICallerProvider
    {
        public override string ProviderName { get; } = ProvidersConstants.TWILIO;

        public TwilioProvider(IOptions<ProvidersSettings> optionsprovider,
            IOptions<AppSettings> optionsApp) : base(optionsprovider, optionsApp)
        {
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
                TwilioClient.Init(settings.AccountSid, settings.AuthToken);
                var to = new PhoneNumber(optionsApp.Value.DestinationPhone);
                var from = new PhoneNumber(settings.PhoneNumber);
                var call = await CallResource.CreateAsync(to, from, url: new Uri(settings.Url));
            }
            catch(Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }
    }
}
