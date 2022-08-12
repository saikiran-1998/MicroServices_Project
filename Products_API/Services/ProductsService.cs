using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products_API.Data;
using Products_API.Interfaces;
using Products_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products_API.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductDbContext context;
        private readonly ILogger<ProductsService> logger;
        private readonly IMapper mapper;
        public ProductsService(ProductDbContext context, ILogger<ProductsService> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
            this.context.Database.EnsureCreated();
        }

        private void SeedData()
        {
            if (!context.Products.Any())
            {
                context.Products.Add(new Data.Product() { Id = 1, Name = "Pen", Price = 20.5M, Inventory = 100 });
                context.Products.Add(new Data.Product() { Id = 2, Name = "Pencil", Price = 10M, Inventory = 70 });
                context.Products.Add(new Data.Product() { Id = 3, Name = "Eraser", Price = 5.5M, Inventory = 120 });
                context.Products.Add(new Data.Product() { Id = 4, Name = "Sharpener", Price = 12.5M, Inventory = 50 });
                context.SaveChanges();
            }
        }

        public async Task<(bool isSucess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await context.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Data.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, "");
                }
                else
                {
                    return (false, null, "Not Found");
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<(bool isSucess, Models.Product products, string ErrorMessage)> GetProductByIdAsync(int id)
        {
            try
            {
                var products = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (products != null)
                {
                    var result = mapper.Map<Data.Product, Models.Product>(products);
                    return (true, result, "");
                }
                else
                {
                    return (false, null, "Not Found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
