using Search_API.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Search_API.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }

        public async Task<(bool isSuccess, dynamic searchResult)> SearchAsync(int customerId)
        {
            var orderResult = await orderService.GetOrdersAsync(customerId);
            var productResult = await productService.GetProductsAsync();
            var customerResult = await customerService.GetCustomerAsync(customerId);
            if (orderResult.isSuccess)
            {
                foreach (var orders in orderResult.orders)
                {
                    foreach (var item in orders.Items)
                    {
                        item.ProductName = productResult.isSucess ?
                            productResult.products.FirstOrDefault(p => p.Id == item.ProductId).Name : "Product name is not available";
                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess ?
                                customerResult.Customer :
                                new { Name = "Customer name is not available" },
                    Orders = orderResult.orders
                };
                return (true, result);
            }
            return (false,null);
        }
    }
}
