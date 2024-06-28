using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string ServiceId { get; set; }
        public int PatientType { get; set; }
        public string ServiceDuration { get; set; }
        public string BeforeTravelTime { get; set; }
        public string AfterTravelTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }

        
    }
}
