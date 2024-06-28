using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class AddEquipmentDto
    {
        public string EquipmentId { get; set; }
        [Required(ErrorMessage = "Please enter Equipment Name")]
        public string EquipmentName { get; set; }

    }
}
