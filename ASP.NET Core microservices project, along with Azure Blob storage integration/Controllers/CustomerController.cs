using Microsoft.AspNetCore.Mvc;
using CustomerDataMigrationProject.Services;
using System.Threading.Tasks;

namespace CustomerDataMigrationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDataService _customerDataService;
        private readonly BlobService _blobService;

        public CustomerController(CustomerDataService customerDataService, BlobService blobService)
        {
            _customerDataService = customerDataService;
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveCustomers()
        {
            var customers = await _customerDataService.GetActiveCustomersAsync();
            return Ok(customers);
        }

        [HttpPost("migrate-legacy")]
        public async Task<IActionResult> MigrateLegacyData()
        {
            var legacyCustomers = await _customerDataService.GetLegacyCustomersAsync();
            foreach (var customer in legacyCustomers)
            {
                await _blobService.UploadLegacyDataAsync($"customer-{customer.Id}.json", Newtonsoft.Json.JsonConvert.SerializeObject(customer));
            }
            return Ok("Legacy data migrated.");
        }
    }
}
