using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders_API.Data;
using Orders_API.Interfaces;
using Orders_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders_API.Services
{
    public class OrderService : IOrderService
    {

        private readonly OrdersDbContext context;
        private readonly ILogger<OrderService> logger;
        private readonly IMapper mapper;

        public OrderService(OrdersDbContext context, ILogger<OrderService> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
            context.Database.EnsureCreated();
        }

        private void SeedData()
        {
            if (!context.Orders.Any())
            {
                context.Orders.Add(new Data.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<Data.OrderItem>()
                    {
                        new Data.OrderItem() { /*OrderId = 1,*/ ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 1,*/ ProductId = 4, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 1,*/ ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 2,*/ ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 3,*/ ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                context.Orders.Add(new Data.Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<Data.OrderItem>()
                    {
                        new Data.OrderItem() { /*OrderId = 1,*/ ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 1,*/ ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 1,*/ ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 2,*/ ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 3,*/ ProductId = 4, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                context.Orders.Add(new Data.Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<Data.OrderItem>()
                    {
                        new Data.OrderItem() { /*OrderId = 1,*/ ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 2,*/ ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { /*OrderId = 3,*/ ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                context.SaveChanges();
            }
        }
        public async Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await context.Orders.Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Data.Order>,
                        IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

    }
}
