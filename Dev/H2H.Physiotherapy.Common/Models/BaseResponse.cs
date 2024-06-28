using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models
{
    public class BaseResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseStatus { get; set; }
        public string Message { get; set; }
        public string CallStartTime { get; set; }
        public string CallEndTime { get; set; }
        public string ServerName { get; set; }

        public static string strFormatDateTime = "yyyy-MM-dd HH:mm";

        public BaseResponse()
        {
            CallStartTime = DateTime.UtcNow.ToString(strFormatDateTime);
            ServerName = Environment.MachineName;
        }

        public void Complete()
        {
            CallEndTime = DateTime.UtcNow.ToString(strFormatDateTime);
        }
    }
}
