using Moq;
using Order_Service.Domain;
using Order_Service.Application;
using Order_Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Order_Service.Domain.Models;
using Order_Service.Application.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Order_Service.Application.DTOs;

namespace Order_Service.Test
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ILogger<IOrderService>> _logger;
        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _logger = new Mock<ILogger<IOrderService>>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _logger.Object);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            CancellationToken cancellationToken = new CancellationToken();
            var orderId = 999;
            _orderRepositoryMock.Setup(repo => repo.GetOrderAsync(orderId, cancellationToken)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.GetOrderByIdAsync(orderId, cancellationToken);

            // Assert
            Assert.Null(result);
        }
    }
}
