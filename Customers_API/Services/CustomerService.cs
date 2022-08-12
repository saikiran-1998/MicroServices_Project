using AutoMapper;
using Customers_API.Data;
using Customers_API.Interfaces;
using Customers_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers_API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext context;
        private readonly ILogger<CustomerService> logger;
        private readonly IMapper mapper;

        public CustomerService(CustomerDbContext customerDbContext, ILogger<CustomerService> logger, IMapper mapper)
        {
            this.context = customerDbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
            this.context.Database.EnsureCreated();
        }

        private void SeedData()
        {
            if (!context.Customers.Any())
            {
                context.Customers.Add(new Data.Customer { Id = 1, Name = "Customer1", Address = "Address1" });
                context.Customers.Add(new Data.Customer { Id = 2, Name = "Customer2", Address = "Address2" });
                context.Customers.Add(new Data.Customer { Id = 3, Name = "Customer3", Address = "Address3" });
                context.Customers.Add(new Data.Customer { Id = 4, Name = "Customer4", Address = "Address4" });
                context.Customers.Add(new Data.Customer { Id = 5, Name = "Customer5", Address = "Address5" });
                context.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await context.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Data.Customer>, IEnumerable<Models.Customer>>(customers);
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

        public async Task<(bool isSuccess, Models.Customer customers, string errorMessage)> GetCustomerByIdAsync(int id)
        {
            try
            {
                var products = await context.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (products != null)
                {
                    var result = mapper.Map<Data.Customer, Models.Customer>(products);
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
