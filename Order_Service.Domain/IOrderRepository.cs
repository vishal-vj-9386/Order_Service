using Order_Service.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Service.Domain
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetAllOrderByCustomerAsync(string customerName, CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetOrdersAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task<Customer> GetCustomerByNameAsync(string customerName, CancellationToken cancellationToken);
        Task<Product> GetProductByNameAsync(string productName, CancellationToken cancellationToken);
        Task<bool> ValidateCustomerAsync(string customerEmail, CancellationToken cancellationToken);
    }
}
