using NG.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NG.BLL.Services.Interfaces
{
    public interface INotificationService
    {
        Task<ModelBase> MakeCall(NotificationModel model);
    }
}
