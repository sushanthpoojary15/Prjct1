using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class PainAssessmentBase
    {
        public string PainAssessmentId { get; set; }
        public string AssessmentId { get; set; }
        public bool PatientPain { get; set; }
        public string PainTool { get; set; }
        public string PainScale { get; set; }
        public string PainType { get; set; }
        public string PainTypeChronic { get; set; }
        public string PainTypeAcute { get; set; }
        public string PainIncreaseWith { get; set; }
        public string PainIncreaseWithOthers { get; set; }
        public string PainDecreaseWith { get; set; }
        public string PainDecreaseWithOthers { get; set; }
        public string BodyPart { get; set; }
        public string OtherFindings { get; set; }
    }


    public class PainAssessmentDto : PainAssessmentBase
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
