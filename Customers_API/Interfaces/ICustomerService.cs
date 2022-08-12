using Customers_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customers_API.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool isSuccess,IEnumerable<Customer> customers,string errorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Customer customers, string errorMessage)> GetCustomerByIdAsync(int id);
    }
}
