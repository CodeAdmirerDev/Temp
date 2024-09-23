
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace MyMicroservice.Services
{
    public class AzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobService(string blobConnectionString, string containerName)
        {
            _blobServiceClient = new BlobServiceClient(blobConnectionString);
            _containerName = containerName;
        }

        public async Task<byte[]> DownloadBlobAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var response = await blobClient.DownloadAsync();
            using var memoryStream = new MemoryStream();
            await response.Value.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
