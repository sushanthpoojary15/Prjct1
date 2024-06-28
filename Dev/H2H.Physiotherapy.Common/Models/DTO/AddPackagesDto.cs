using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class AddPackagesDto 
    {
        public string PackagesId { get; set; }

        public string PackageName { get; set; }
        public string EmiratesId { get; set; }
       
        public int Amount { get; set; }
   
    }
}
