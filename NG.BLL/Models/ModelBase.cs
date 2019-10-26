using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NG.BLL.Models
{
    public class ModelBase
    {
        public bool IsValid
        {
            get
            {
                return !Errors.Any();
            }
        }

        public IList<string> Errors { get; set; } = new List<string>();
        public IList<string> Infos { get; set; } = new List<string>();

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddInfo(string info)
        {
            Infos.Add(info);
        }
    }
}
