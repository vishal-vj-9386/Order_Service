using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order_Service.Application.DTOs;
using Order_Service.Domain;
using Order_Service.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Service.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<IOrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<IOrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrderByCustomerAsync(string customerName, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderRepository.GetAllOrderByCustomerAsync(customerName, cancellationToken);

                if(orders == null)
                {
                    _logger.LogInformation($"No orders are placed by customer {customerName}");
                    return null;
                }

                return orders.Select(order => new OrderDto
                {
                    OrderId = order.Id,
                    CustomerName = order.Customer.Name,
                    ProductName = order.Product.Name,
                    Quantity = order.Quantity,
                    Total = order.Total,
                    CreatedAt = order.OrderDate
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting customer's all orders. Error Message - {ex.Message}");
                return null;
            }
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRepository.GetOrderAsync(id, cancellationToken);

                if (order == null)
                {
                    _logger.LogInformation($"Given order id is not present. Order ID - {id}");
                    return null;
                }

                return new OrderDto 
                {
                    OrderId = order.Id,
                    CustomerName = order.Customer.Name,
                    ProductName = order.Product.Name,
                    Quantity = order.Quantity,
                    Total = order.Total,
                    CreatedAt = order.OrderDate
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting order by id. Error Message - {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersAsync(pageNumber, pageSize, cancellationToken);

                return orders.Select(order => new OrderDto
                {
                    OrderId = order.Id,
                    CustomerName = order.Customer.Name,
                    ProductName = order.Product.Name,
                    Quantity = order.Quantity,
                    Total = order.Total,
                    CreatedAt = order.OrderDate
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in pagination of order data. Error Message - {ex.Message}");
                return null;
            }
        }

        public async Task PlaceOrderAsync(OrderInputDto orderInputDto, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _orderRepository.GetCustomerByNameAsync(orderInputDto.CustomerName, cancellationToken);

                if(customer == null)
                {
                    _logger.LogError("requested customer is not present in database");
                    return;
                }
                var product = await _orderRepository.GetProductByNameAsync(orderInputDto.ProductName, cancellationToken);

                if (product == null)
                {
                    _logger.LogError("requested product is not present in database");
                    return;
                }

                if (customer != null && product != null)
                {
                    var order = new Order
                    {
                        CustomerId = customer.Id,
                        ProductId = product.Id,
                        Quantity = orderInputDto.Quantity,
                        OrderDate = DateTime.Now,
                        Total = orderInputDto.Quantity * product.Price
                    };

                    await _orderRepository.CreateOrderAsync(order, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in placing the order. Error Message - {ex}");
            }
        }

        public async Task<bool> ValidateCustomer(string customerEmail, CancellationToken cancellationToken)
        {
            return await _orderRepository.ValidateCustomerAsync(customerEmail, cancellationToken);
        }
    }
}
