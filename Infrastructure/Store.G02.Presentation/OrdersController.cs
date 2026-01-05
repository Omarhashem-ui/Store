using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        public OrdersController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync(OrderRequest request)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _servicesManager.OrderServices.GetOrderAsync(request, userEmailClaim.Value);
            return Ok(result);
        }
        [HttpGet("deliverymethods")]
        public async Task<IActionResult> GetAllDeliveryMethodAsync()
        {
            var result = await _servicesManager.OrderServices.GetAllDeliveryMethodAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderByIdForSpecificUserAsync(Guid id)
        {
            var userEmailClaim=User.FindFirst(ClaimTypes.Email); 
            var result = await _servicesManager.OrderServices.GetOrderByIdForSpecificUserAsync(id,userEmailClaim.Value);
            return Ok(result);
        }
        [HttpGet("userorders")]
        [Authorize]
        public async Task<IActionResult> GetOrdersForSpecificUserAsync()
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
         var result =  await _servicesManager.OrderServices.GetOrdersForSpecificUserAsync(userEmailClaim.Value);
            return Ok(result);
        }
       
    }
}
