using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class PackagesModel
    {
       
        public int Id { get; set; }
        public string PackagesId { get; set; }

        public string PackageName { get; set; }
        public string Emirates { get; set; }  
        public string EmiratesName { get; set; }    
        public string EmiratesId { get; set; }
        public int Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
