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
    public class EnquiryStore : IEnquiryStore
    {
        private readonly IDatabaseManager _databaseManager;

        public EnquiryStore(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;

        }
        public async Task UpsertEnquiry(EnquiryDTO enquiry, string currentUser)
        {
            string commandText = "usp_UpsertEnquiry";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@EnquiryId", enquiry.EnquiryId, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@FirstName", enquiry.FirstName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@MiddleName", enquiry.MiddleName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@LastName", enquiry.LastName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@Gender", enquiry.Gender, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@DOB", enquiry.DOB, SqlDbType.Date),
                _databaseManager.CreateParameter("@Age", enquiry.Age, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@Address", enquiry.Address, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@City", enquiry.City, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@Emirates", enquiry.Emirates, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@Country", enquiry.Country, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@GoogleMapLink", enquiry.GoogleMapLink, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PayerName", enquiry.PayerName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@EmergencyContactName", enquiry.EmergencyContactName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PayerContactNumber", enquiry.PayerContactNumber, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@EmergencyContactNumber", enquiry.EmergencyContactNumber, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PatientType", enquiry.PatientType, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@EnquiryType", enquiry.EnquiryType, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PTSupervisor", enquiry.PTSupervisor, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PhysiotherapyType", enquiry.PhysiotherapyType, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@NoOfSessions", enquiry.NoOfSessions, SqlDbType.Int),
                _databaseManager.CreateParameter("@Comments", enquiry.Comments, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@CreatedBy", currentUser, SqlDbType.NVarChar),

            };

            await _databaseManager.InsertOrUpdateWithTransaction(commandText, parameters);
        }
    }
}