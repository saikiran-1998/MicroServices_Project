using Orders_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders_API.Interfaces
{
    public interface IOrderService
    {
        Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> GetOrdersAsync(int customerId);
    }
}
