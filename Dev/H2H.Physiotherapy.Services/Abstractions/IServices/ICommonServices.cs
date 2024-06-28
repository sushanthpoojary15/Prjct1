using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IServices
{
    public interface ICommonServices
    {
        public Task <int> Generate8DigitReferenceNumber();
        public Task<int> Generate6DigitOtp();
    }
}
