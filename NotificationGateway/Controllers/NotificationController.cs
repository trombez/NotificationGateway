using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NG.BLL.Configuration;
using NG.BLL.Models;
using NG.BLL.Services.Interfaces;

namespace NotificationGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;
        private readonly IOptions<AppSettings> optionsApp;

        public NotificationController(INotificationService notificationService,
            IOptions<AppSettings> optionsApp)
        {
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            this.optionsApp = optionsApp ?? throw new ArgumentNullException(nameof(optionsApp));
        }

        [HttpPost("MakeCall")]
        public async Task<IActionResult> MakeCall([FromBody]NotificationModel model)
        {
            if (!model.UserName.Equals(optionsApp.Value.UserName))
                return BadRequest();

            if (!model.Password.Equals(optionsApp.Value.Password))
                return BadRequest();

            var result = await notificationService.MakeCall(model).ConfigureAwait(false);
            if (result.IsValid)
                return Ok();
            else return BadRequest();
        }
    }
}