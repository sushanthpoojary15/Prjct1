using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IDataStore
{
    public interface IAssessmentStore
    {
        public Task<List<PainAssessmentDto>> GetPainAssessment();
        public Task<List<PainAssessmentDto>> GetPainAssessmentById(string painAssessmentId);

        public Task UpsertPainAssessment(PainAssessmentBase painAssessmentDto, string updatedBy);

        public Task DeletePainAssessment(string painAssessmentId);  
    }
}
