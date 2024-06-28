using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using H2H.Physiotherapy.Services.Abstractions.OtherServices;
using H2H.Physiotherapy.Services.Configs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Features.OtherServices
{
    public class BlobFileData
    {
        public Stream File { set; get; }
        public string container { set; get; }
        public string blobName { set; get; }
        public string ContentType { set; get; }
    }




    public class FileUploadService : IFileUploadService
    {
        private readonly BlobStorageSettings blobStorageSettings;
        private IMemoryCache _memoryCache;
        private readonly BlobContainerClient _blobContainerClient;
       

        public FileUploadService(IOptions<PhysioTheraphyConfigurations> configuration, IMemoryCache memoryCache,BlobServiceClient blobServiceClient)
        {
            blobStorageSettings = configuration.Value.BlobStorageSettings;
            _memoryCache = memoryCache;
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageSettings.StorageBlobContainer);

        }

        public async Task<string> UploadImage(BlobFileData message)
        {
            BlobFileData BlobData = message;

            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.None);
            var blobClient = _blobContainerClient.GetBlobClient(BlobData.blobName);
            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = BlobData.ContentType
            };

            // Upload the file to the blob
            using (var stream = BlobData.File)
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions
                {
                    HttpHeaders = blobHttpHeaders
                });
            }
           
            return blobClient.Uri.AbsoluteUri;
        }
        public async Task<string> UploadImageToBlobInBackgroundThread(BlobFileData mailMessage)
        {
            string ImgPath = await UploadImage(mailMessage);
            return ImgPath;
        }

        public async Task AppendData(BlobFileData blobFileData, string appendString)
        {
            string Account = blobStorageSettings.StorageConnectionString;
            string BlobContainer = blobStorageSettings.StorageBlobContainer;
            try
            {
                var container = CloudStorageAccount.Parse(Account).CreateCloudBlobClient().GetContainerReference(BlobContainer.ToLower());
                var appendBlob = container.GetAppendBlobReference(blobFileData.blobName);
                if (!await appendBlob.ExistsAsync())
                {
                    await appendBlob.CreateOrReplaceAsync();
                }

                byte[] dataBytes = Encoding.UTF8.GetBytes(appendString);
                using (MemoryStream stream = new MemoryStream(dataBytes))
                {
                    await appendBlob.AppendBlockAsync(stream);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static string GenerateColor()
        {
            string htmlHexColor = "";
            //using (Health2HomeContext DBContext = new Health2HomeContext())
            //{
            //    Random randomGen = new Random();
            //    KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            //    KnownColor randomColorName = names[randomGen.Next(names.Length)];
            //    Color randomColor = Color.FromKnownColor(randomColorName);
            //    // String strHtmlColor = System.Drawing.ColorTranslator.ToHtml(randomColor);
            //    string htmlHexColorValueThree = String.Format("#{0:X2}{1:X2}{2:X2}", randomColor.R, randomColor.G, randomColor.B);
            //    if (DBContext.mstUsers.Where(x => x.IsActive == true && x.IdentityColor == htmlHexColorValueThree).Count() > 0)
            //        htmlHexColor = GenerateColor();
            return htmlHexColor;
        }

        public async Task<string> GenerateSharedAcessSignatureForBlobContainer()
        {
            string cacheKey = "BlobSasToken";
            if (!_memoryCache.TryGetValue(cacheKey, out string sasToken))
            {


                string Account = blobStorageSettings.StorageConnectionString;
                string BlobContainer = blobStorageSettings.StorageBlobContainer;
                int expiryTimeSasToken = blobStorageSettings.SasTokenExpiry;
                int expiryTimeCache = blobStorageSettings.SasTokenCacheExpiry;
                var container = CloudStorageAccount.Parse(Account).CreateCloudBlobClient().GetContainerReference(BlobContainer.ToLower());
                var saspolicy = new SharedAccessBlobPolicy
                {
                    Permissions = SharedAccessBlobPermissions.Read,
                    SharedAccessStartTime = DateTime.UtcNow,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(expiryTimeSasToken)

                };
                sasToken = container.GetSharedAccessSignature(saspolicy);
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiryTimeCache)
                };
                _memoryCache.Set(cacheKey, sasToken, cacheEntryOptions);
            }
            return sasToken;

        }
    }
}

