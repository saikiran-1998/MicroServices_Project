using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orders_API.Interfaces;
using System.Threading.Tasks;

namespace Orders_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var orders = await orderService.GetOrdersAsync(customerId);
            if (orders.isSuccess)
            {
                return Ok(orders.orders);
            }
            return NotFound();
        }

    }
}
