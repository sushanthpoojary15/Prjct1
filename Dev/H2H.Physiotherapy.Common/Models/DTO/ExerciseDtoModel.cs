using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class ExerciseDtoModel
    {

        public string ExerciseId { get; set; }

        [Required(ErrorMessage = "Please enter the Name of Exercise ")]
        [MaxLength(50)]
        public string Name { get; set; }

       
    }
}
