using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class OtpDTO
    {
        public string UserName { get; set; }

        public int ReferenceNumber { get; set; }

        public int OtpValue { get; set; } 

        public int OtpValidity { get; set; }
    }
}
