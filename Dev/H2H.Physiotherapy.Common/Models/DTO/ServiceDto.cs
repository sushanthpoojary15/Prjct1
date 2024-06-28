using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class ServiceDto
    {
        public int PatientType { get; set; }

        [MaxLength(15)]
        public string ServiceDuration { get; set; }

        [MaxLength(10)]
        public string BeforeTravelTime { get; set; }

        [MaxLength(10)]
        public string AfterTravelTime { get; set; }

    }

    public class ServiceUpdateDto
    {
        [MaxLength(15)]
        public string ServiceDuration { get; set; }

        [MaxLength(10)]
        public string BeforeTravelTime { get; set; }

        [MaxLength(10)]
        public string AfterTravelTime { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
