using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NG.BLL.Services.Interfaces;
using Microsoft.Extensions.Options;
using NG.BLL.Configuration;
using NG.BLL.Models;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http;
using System.Text;
using NG.BLL.Extensions;

namespace NotificationFunction
{
    public class NotificationFunction
    {
        private readonly INotificationService notificationService;
        private readonly IOptions<AppSettings> optionsApp;

        public NotificationFunction(INotificationService notificationService,
            IOptions<AppSettings> optionsApp)
        {
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            this.optionsApp = optionsApp ?? throw new ArgumentNullException(nameof(optionsApp));
        }


        [FunctionName("NotificationFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<NotificationModel>(requestBody);

            var output = new ModelBase();
            try
            {
                if (!model.UserName.Equals(optionsApp.Value.UserName))
                    output.AddError("Wrong Username");

                if (!model.Password.Equals(optionsApp.Value.Password))
                    output.AddError("Wrong Password");

                if (!output.IsValid)
                    return new BadRequestObjectResult(output.GetJson());

                var result = await notificationService.MakeCall(model).ConfigureAwait(false);
                output.Errors = result.Errors;

                return (ActionResult)new OkObjectResult(output.GetJson());
            }
            catch (Exception ex)
            {
                output.AddError(ex.Message);
                return new BadRequestObjectResult(output.GetJson());
            }
        }
    }
}
