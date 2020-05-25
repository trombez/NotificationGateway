using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NG.BLL.Extensions
{
    public static class FileFormatExtension
    {
        public static string GetJson(this object currentObject)
        {
            try
            {
                return JsonConvert.SerializeObject(currentObject);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
