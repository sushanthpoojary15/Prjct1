using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IDataStore
{
    public interface IEnquiryStore
    {
        public Task UpsertEnquiry(EnquiryDTO enquiry, string currentUser);
    }
}
