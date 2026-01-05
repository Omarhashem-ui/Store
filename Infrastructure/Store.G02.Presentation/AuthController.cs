using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        public AuthController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }
        [HttpPost ("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
           var result =await _servicesManager.AuthServices.LoginAsync(request);  

            return Ok(result);
        }
        [HttpPost ("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
           var result =await _servicesManager.AuthServices.RegisterAsync(request);  

            return Ok(result);
        }
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
           var result =await  _servicesManager.AuthServices.CheckEmailExistsAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser ()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result =  await _servicesManager.AuthServices.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
          var result= await _servicesManager.AuthServices.GetCurrentUserAddressAsync(email.Value);
         return Ok(result);
        }
        [Authorize]
        [HttpPost("Address")]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressResponse request)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _servicesManager.AuthServices.UpdateCurrentUserAddressAsync(request, email.Value);
            return Ok(result);
        }

    }
}
