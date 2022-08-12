using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Search_API.Interfaces;
using Search_API.Models;
using System.Threading.Tasks;

namespace Search_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
         private readonly ISearchService isearchservice;
        public SearchController(ISearchService isearchservice)
        {
            this.isearchservice = isearchservice;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result =await this.isearchservice.SearchAsync(term.CustomerId);
            if (result.isSuccess)
            {
                return Ok(result.searchResult);
            }
            return NotFound();
        }
    }
}
