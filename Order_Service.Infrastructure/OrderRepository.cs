using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order_Service.Domain;
using Order_Service.Domain.Models;
using Order_Service.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Service.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext orderDbContext) 
        { 
            _dbContext = orderDbContext;
        }
        public async Task CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrderByCustomerAsync(string customerName, CancellationToken cancellationToken)
        {
            var orders = from o in _dbContext.Orders
                         join c in _dbContext.Customers on o.CustomerId equals c.Id
                         where c.Name.ToLower() == customerName.ToLower()
                         select o;

            return orders;
        }

        public async Task<Customer> GetCustomerByNameAsync(string customerName, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync
                (c => c.Name.ToLower() == customerName.ToLower(), cancellationToken);
            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public async Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order =  await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

            if(order == null)
            {
                return null;
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders.Skip((page) - 1).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<Product> GetProductByNameAsync(string productName, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == productName.ToLower(), cancellationToken);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<bool> ValidateCustomerAsync(string customerEmail, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.AnyAsync(c => c.Email == customerEmail, cancellationToken);
        }
    }
}
