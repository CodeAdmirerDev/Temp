
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using System.IO;

namespace MyMicroservice.Services
{
    public class DataService : IDataService
    {
        private readonly AzureSqlService _azureSqlService;
        private readonly AzureBlobService _azureBlobService;

        public DataService(AzureSqlService azureSqlService, AzureBlobService azureBlobService)
        {
            _azureSqlService = azureSqlService;
            _azureBlobService = azureBlobService;
        }

        // Get Active Data from Azure SQL
        public async Task<IEnumerable<DataModel>> GetActiveDataAsync()
        {
            return await _azureSqlService.GetActiveDataAsync();
        }

        // Get Legacy Data from Azure Blob Storage
        public async Task<byte[]> GetLegacyDataAsync(string fileName)
        {
            return await _azureBlobService.DownloadBlobAsync(fileName);
        }
    }
}
