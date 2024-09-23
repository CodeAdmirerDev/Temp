
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // Get Active Data from Azure SQL
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveData()
        {
            var data = await _dataService.GetActiveDataAsync();
            return Ok(data);
        }

        // Get Legacy Data from Azure Blob Storage
        [HttpGet("legacy/{fileName}")]
        public async Task<IActionResult> GetLegacyData(string fileName)
        {
            var legacyData = await _dataService.GetLegacyDataAsync(fileName);
            if (legacyData == null)
                return NotFound();

            return File(legacyData, "application/octet-stream", fileName);
        }
    }
}
