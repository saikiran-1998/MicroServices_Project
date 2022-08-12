using System.Threading.Tasks;

namespace Search_API.Interfaces
{
    public interface ISearchService
    {
      Task<(bool isSuccess,dynamic searchResult)>  SearchAsync(int customerId);
    }
}
