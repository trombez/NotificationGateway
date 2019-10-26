using Microsoft.Extensions.Options;
using NG.BLL.Configuration;
using NG.BLL.Extensions;
using NG.BLL.Models;
using NG.BLL.Providers.Interfaces;
using NG.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace NG.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IOptions<AppSettings> optionsApp;
        private readonly IServiceProvider services;

        public NotificationService(IOptions<AppSettings> optionsApp,
            IServiceProvider services)
        {
            this.optionsApp = optionsApp ?? throw new ArgumentNullException(nameof(optionsApp));
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public virtual async Task<ModelBase> MakeCall(NotificationModel model)
        {
            var result = new ModelBase();
            string providerName = optionsApp.Value.ProviderName;

            if (!string.IsNullOrEmpty(model.Provider))
                providerName = model.Provider;

            var provider = services.GetProvider<ICallerProvider>(providerName);

            if (provider == null)
            {
                result.AddError("Provider not found");
                return result;
            }

            result = await provider.MakeCall(model).ConfigureAwait(false);
            return result;
        }
    }
}
