using Microsoft.Extensions.Logging;
using Search_API.Interfaces;
using Search_API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Search_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductService> logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool isSucess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync()
        {

            try
            {
                var client = httpClientFactory.CreateClient("ProductService");
                var response = await client.GetAsync($"products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                    return (true, result, "");
                }
                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
