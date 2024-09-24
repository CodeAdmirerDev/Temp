using Microsoft.AspNetCore.Mvc;
using CustomerDataService.Services;
using System.Threading.Tasks;

namespace CustomerDataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDataService _customerDataService;

        public CustomerController(CustomerDataService customerDataService)
        {
            _customerDataService = customerDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveCustomers()
        {
            var customers = await _customerDataService.GetActiveCustomersAsync();
            return Ok(customers);
        }

        [HttpPost("segregate")]
        public async Task<IActionResult> SegregateData()
        {
            var customers = await _customerDataService.GetActiveCustomersAsync();
            foreach (var customer in customers)
            {
                await _customerDataService.SaveLegacyCustomerToBlobAsync(customer);
            }
            return Ok("Data segregated.");
        }
    }
}
