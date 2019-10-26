using NG.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NG.BLL.Providers.Interfaces
{
    public interface ICallerProvider : IProvider
    {
        Task<ModelBase> MakeCall(NotificationModel model);
    }
}
