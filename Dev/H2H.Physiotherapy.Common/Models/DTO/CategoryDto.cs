using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
        public class CategoryDto
        {
            public string CategoryId { get; set; }

            [Required(ErrorMessage = "Name is requried")]
            public string Name { get; set; }

        }
}
