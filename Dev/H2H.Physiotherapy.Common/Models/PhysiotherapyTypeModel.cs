using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class PhysiotherapyTypeModel
    {
        
        public int Id { get; set; }

        public string PhysiotherapyTypeId { get; set; }

        [Required(ErrorMessage = "Please mention the PhsiotheraphyName")]
        [MaxLength(200)]
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set;}
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set;} 
        public bool IsActive { get; set; }


    }
}
