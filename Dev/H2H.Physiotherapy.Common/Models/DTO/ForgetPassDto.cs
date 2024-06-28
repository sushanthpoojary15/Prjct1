using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class ForgetPassDto
    {
        public string UserName {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
    }
}
