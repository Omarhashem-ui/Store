using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServicesManager _servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
          var result = await _servicesManager.BasketServices.GetBasketByIdAsync(id);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(CustomerBasketDto dto)
        {
          var result = await _servicesManager.BasketServices.CreateOrUpdateBaketAsync(dto,TimeSpan.FromDays(1));
            return Ok(result);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
           await _servicesManager.BasketServices.DeleteBasketAsync(id);
            return NoContent();

        }
    }
}
