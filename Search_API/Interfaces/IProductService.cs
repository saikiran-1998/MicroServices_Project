using Search_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search_API.Interfaces
{
    public interface IProductService
    {
        Task<(bool isSucess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
    }
}
