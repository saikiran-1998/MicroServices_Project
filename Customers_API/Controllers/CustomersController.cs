using Customers_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Customers_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomersController(ICustomerService customer)
        {
            this.customerService = customer;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await this.customerService.GetCustomersAsync();
            if (result.isSuccess)
            {
                return Ok(result.customers);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersByIdAsync(int id)
        {
            var result = await this.customerService.GetCustomerByIdAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.customers);
            }
            return NotFound();
        }
    }
}
