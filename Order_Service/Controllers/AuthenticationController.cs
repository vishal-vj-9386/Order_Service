using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Order_Service.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Order_Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IOrderService _orderService;
        public AuthenticationController(IConfiguration config, IOrderService orderService)
        {
            _config = config;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> TokenAsync([FromBody] Login loginRequest, CancellationToken cancellationToken)
        {
            var isAuthorize = await _orderService.ValidateCustomer(loginRequest.Email, cancellationToken);

            if (!isAuthorize)
            {
               return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(null,
              null,
              null,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}
