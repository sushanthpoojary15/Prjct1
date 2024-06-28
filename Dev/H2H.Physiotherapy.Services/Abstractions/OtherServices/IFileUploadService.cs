

using H2H.Physiotherapy.Services.Features.OtherServices;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.OtherServices
{
    public interface IFileUploadService
    {
        public Task<string> UploadImageToBlobInBackgroundThread(BlobFileData mailMessage);
        public Task<string> UploadImage(BlobFileData message);
        public Task AppendData(BlobFileData blobFileData, string appendString);

        public Task<string> GenerateSharedAcessSignatureForBlobContainer();

    }
}
