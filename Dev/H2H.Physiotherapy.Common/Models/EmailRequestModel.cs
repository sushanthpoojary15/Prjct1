using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class EmailRequestModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string HTMLBody { get; set; }
        public bool IsCC { get; set; }
    }
}
