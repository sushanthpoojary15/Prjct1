using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IServices
{
    public interface IEnquiryService
    {
        public Task<ApiResponse> UpsertEnquiry(EnquiryDTO enquiryDTO);
    }
}
