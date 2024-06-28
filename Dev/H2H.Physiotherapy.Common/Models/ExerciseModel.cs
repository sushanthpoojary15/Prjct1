using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class ExerciseModel
    {
    
        public int Id { get; set; }

        public string ExerciseId { get; set; }

        [Required(ErrorMessage ="Please enter the Name of Exercise ")]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } 

        public int UpdatedBy { get; set; } 

        public DateTime UpdatedOn { get; set; } 

        public bool IsActive { get; set; } 
    }
}
