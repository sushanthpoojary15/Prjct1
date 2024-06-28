using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Logging
{
    public interface ILoggerService
    {
        public void LogError(object obj, Exception ex, string context = "No context available");
    }
}
