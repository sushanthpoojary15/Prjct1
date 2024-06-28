using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.OtherServices
{
    public interface IEmailService
    {
        public Task SendAutomatedMailwithCustomCC(string to, string Subject, string HTMLBody, bool IsCC = false, string cc = "");
        public Task SendAutomatedMail(string to, string Subject, string HTMLBody, bool IsCC = false);
    }
}
