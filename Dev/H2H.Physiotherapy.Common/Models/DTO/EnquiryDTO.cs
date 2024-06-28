using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class EnquiryDTO
    {
        public string EnquiryId { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 200 characters")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 200 characters")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "Please enter Emirates")]
        public string Emirates { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        public string Country { get; set; }
        public string GoogleMapLink { get; set; }

        [Required(ErrorMessage = "Please enter Payer Name")]
        public string PayerName { get; set; }

        [Required(ErrorMessage = "Please enter Emergency contact Name")]
        public string EmergencyContactName { get; set; }
        public string PayerContactNumber { get; set; }
        public string EmergencyContactNumber { get; set; }

        [Required(ErrorMessage = "Please enter patient type Id")]
        public string PatientType { get; set; }

        [Required(ErrorMessage = "Please enter Enquiry type Id")]
        public string EnquiryType { get; set; }
        public string PTSupervisor { get; set; }

        [Required(ErrorMessage = "Please enter physiotherapy type Id")]
        public string PhysiotherapyType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of sessions must be greater than 0")]
        public int NoOfSessions { get; set; }
        public string Comments { get; set; }
    }
}
