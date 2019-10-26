using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NG.BLL.Configuration
{
    public class ProvidersSettings
    {
        public List<ProviderSettings> ProvidersList { get; set; } = new List<ProviderSettings>();

        public ProviderSettings this[string key]
        {
            get {
                return ProvidersList.FirstOrDefault(s => s.ProviderName.Equals(key));
            }
        }
    }
}
