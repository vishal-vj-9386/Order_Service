using Order_Service.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Service.Application.Services
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(OrderInputDto orderInputDto, CancellationToken cancellationToken);
        Task<OrderDto> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<OrderDto>> GetOrdersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<IEnumerable<OrderDto>> GetAllOrderByCustomerAsync(string customerName, CancellationToken cancellationToken);
        Task<bool> ValidateCustomer(string customerEmail, CancellationToken cancellationToken);
    }
}
