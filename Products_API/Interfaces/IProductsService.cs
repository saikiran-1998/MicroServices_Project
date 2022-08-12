using Products_API.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products_API.Interfaces
{
    public interface IProductsService
    {
        Task<(bool isSucess,IEnumerable<Product> products,string ErrorMessage)> GetProductsAsync();
        Task<(bool isSucess, Product products, string ErrorMessage)> GetProductByIdAsync(int id);

    }
}
