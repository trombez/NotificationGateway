using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NG.BLL.ApiClient.Interfaces
{
    public interface IIFTTTClient
    {
        Task<string> MakeCall(string address);
    }
}
