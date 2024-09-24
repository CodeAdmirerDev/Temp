using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace CustomerDataMigrationProject.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task UploadLegacyDataAsync(string blobName, string data)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("legacy-data");
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data)))
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
        }

        public async Task<string> GetLegacyDataAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("legacy-data");
            var blobClient = containerClient.GetBlobClient(blobName);

            var response = await blobClient.DownloadAsync();
            using (var reader = new StreamReader(response.Value.Content))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
