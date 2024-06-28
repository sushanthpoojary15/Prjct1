using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public  class UserDTO:UserAddDTO
    {
        public string CreatedByName { get; set; }

        public string UpdatedByName { get; set; }
    }
}
