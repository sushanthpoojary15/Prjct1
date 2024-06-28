using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class EnquiryType

    {
        public int Id { get; set; }
        public string TypeOfEnquiryId { get; set; }
        [Required(ErrorMessage = "Please mention the Enquiry Name")]
        [MaxLength(200)]
        public string TypeOfEnquiryName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
