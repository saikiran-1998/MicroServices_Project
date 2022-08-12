using Microsoft.AspNetCore.Mvc;
using Products_API.Interfaces;
using System.Threading.Tasks;

namespace Products_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController :ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await this.productsService.GetProductsAsync();
            if (result.isSucess)
            {
                return Ok(result.products);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var result = await this.productsService.GetProductByIdAsync(id);
            if (result.isSucess)
            {
                return Ok(result.products);
            }
            return NotFound();
        }
    }
}
