using System;
using System.Collections.Generic;
using System.Text;

namespace NG.BLL.Models
{
    public class BaseInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
    }
}
