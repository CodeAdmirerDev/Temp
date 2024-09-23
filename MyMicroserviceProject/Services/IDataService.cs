
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMicroservice.Services
{
    public interface IDataService
    {
        Task<IEnumerable<DataModel>> GetActiveDataAsync();  // From Azure SQL
        Task<byte[]> GetLegacyDataAsync(string fileName);   // From Azure Blob Storage
    }
}
