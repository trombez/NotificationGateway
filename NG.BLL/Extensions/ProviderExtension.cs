using Microsoft.Extensions.DependencyInjection;
using NG.BLL.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NG.BLL.Extensions
{
    public static class ProviderExtension
    {
        public static T GetProvider<T>(this IServiceProvider serviceCollection, string providerName)
            where T : IProvider
        {
            try
            {
                var services = serviceCollection.GetServices<T>();
                return services.First(o => o.ProviderName.Equals(providerName));
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }
    }
}
