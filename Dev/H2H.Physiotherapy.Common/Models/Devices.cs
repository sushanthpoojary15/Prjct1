using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class Devices
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }
    }
}
