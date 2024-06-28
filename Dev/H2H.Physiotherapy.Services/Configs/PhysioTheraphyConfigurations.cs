using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Configs
{
    public class PhysioTheraphyConfigurations
    {
        public string ConnectionString { get; set; }
        public TokenSettings TokenSettings { get; set; }
        public BlobStorageSettings BlobStorageSettings { get; set; }
        public EmailSettings EmailSettings { get; set; }
        public string SiteUrl { get; set; }

        public OtpSettings otpSettings { get; set; }
    }

    public class BlobStorageSettings
    {
        public string StorageConnectionString { get; set; }
        public string StorageBlobContainer { get; set; }

        public int SasTokenExpiry { get; set; }

        public int SasTokenCacheExpiry { get; set; }
    }

    public class TokenSettings
    {
        public string TokenSigningKey { get; set; }
        public int TokenExpiryTime { get; set; }
    }

    public class EmailSettings
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool enableSsl { get; set; }
        public int Port { get; set; }
        public string CRFDemandCCListRecipients { get; set; }
    }
    public class OtpSettings
    {
        public int OtpExpiry { get; set; }
    
    }
}
