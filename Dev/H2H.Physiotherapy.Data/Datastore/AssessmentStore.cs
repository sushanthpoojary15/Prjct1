using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Data.Abstractions;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Data.Datastore
{
    public class AssessmentStore : IAssessmentStore
    {
        private readonly IDatabaseManager _databaseManager;

        public AssessmentStore(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<List<PainAssessmentDto>> GetPainAssessment()
        {
            string commandText = "usp_GetPainAssessment";

            var painAssessmentList = await _databaseManager.GetAllColumns<PainAssessmentDto>(commandText);

            return painAssessmentList.ToList();
        }

        public async Task<List<PainAssessmentDto>> GetPainAssessmentById(string painAssessmentId)
        {
            string commandText = "usp_GetPainAssessmentById";

            IDbDataParameter[] parameters = new IDbDataParameter[]
          {
                _databaseManager.CreateParameter("@PainAssessmentId",painAssessmentId , SqlDbType.NVarChar)
          };

            var painAssessmentList = await _databaseManager.GetAllColumns<PainAssessmentDto>(commandText, parameters);

            return painAssessmentList.ToList();
        }

        public async Task UpsertPainAssessment(PainAssessmentBase painAssessmentDto, string updatedBy)
        {
            string commandText = "usp_UpsertPainAssessment";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@PainAssessmentId", string.IsNullOrEmpty(painAssessmentDto.PainAssessmentId) ? (object)DBNull.Value : painAssessmentDto.PainAssessmentId, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@AssessmentId", painAssessmentDto.AssessmentId, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PatientPain", painAssessmentDto.PatientPain, SqlDbType.Bit),
                _databaseManager.CreateParameter("@PainTool", painAssessmentDto.PainTool, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainScale", painAssessmentDto.PainScale, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainType", painAssessmentDto.PainType, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainTypeChronic", painAssessmentDto.PainTypeChronic, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainTypeAcute", painAssessmentDto.PainTypeAcute, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainIncreaseWith", painAssessmentDto.PainIncreaseWith, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainIncreaseWithOthers", painAssessmentDto.PainIncreaseWithOthers, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainDecreaseWith", painAssessmentDto.PainDecreaseWith, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PainDecreaseWithOthers", painAssessmentDto.PainDecreaseWithOthers, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@BodyPart", painAssessmentDto.BodyPart, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@OtherFindings", painAssessmentDto.OtherFindings, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@UpdatedBy", updatedBy, SqlDbType.NVarChar)
            };

            await _databaseManager.InsertOrUpdateWithTransaction(commandText, parameters);
        }

        public async Task DeletePainAssessment(string painAssessmentId)
        {
            string commandText = "usp_DeletePainAssessment";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@PainAssessmentId", painAssessmentId, SqlDbType.NVarChar)
            };
            await _databaseManager.Delete(commandText, parameters);
        }

    }


}
