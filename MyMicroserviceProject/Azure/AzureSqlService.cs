
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMicroservice.Services
{
    public class AzureSqlService
    {
        private readonly MyDbContext _dbContext;

        public AzureSqlService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DataModel>> GetActiveDataAsync()
        {
            return await _dbContext.DataModels.ToListAsync();
        }
    }
}
