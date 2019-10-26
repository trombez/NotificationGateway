using Microsoft.Extensions.Options;
using NG.BLL.Configuration;
using NG.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NG.BLL.Providers
{
    public abstract class ProviderBase
    {
        protected readonly IOptions<ProvidersSettings> optionsprovider;
        protected readonly IOptions<AppSettings> optionsApp;

        public abstract string ProviderName { get; }

        public ProviderBase(IOptions<ProvidersSettings> optionsprovider,
            IOptions<AppSettings> optionsApp)
        {
            this.optionsprovider = optionsprovider ?? throw new ArgumentNullException(nameof(optionsprovider));
            this.optionsApp = optionsApp ?? throw new ArgumentNullException(nameof(optionsApp));
        }

        public virtual ProviderResult GetSettings()
        {
            var result = new ProviderResult();

            var settings = optionsprovider.Value[ProviderName];
            if (settings == null)
            {
                result.AddError($"Settings not found for provider {ProviderName}");
                return result;
            }

            result.ProviderSettings = settings;
            return result;
        }
    }
}
