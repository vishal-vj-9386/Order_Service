using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order_Service.Application.DTOs;
using Order_Service.Application.Services;

namespace Order_Service.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderInputDto orderInputDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _orderService.PlaceOrderAsync(orderInputDto, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersByCustomerName([FromQuery] string customerName, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllOrderByCustomerAsync(customerName, cancellationToken);

            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderById([FromQuery] int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int startRecord, [FromQuery] int recordSize, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrdersAsync(startRecord, recordSize, cancellationToken);

            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

    }
}
