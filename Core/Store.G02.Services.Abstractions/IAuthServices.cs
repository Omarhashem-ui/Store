using Store.G02.Shared.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IAuthServices
    {
     Task<UserResponse>  LoginAsync(LoginRequest request);
     Task<UserResponse>  RegisterAsync(RegisterRequest request);   
     Task<bool> CheckEmailExistsAsync(string email);
     Task<UserResponse?> GetCurrentUserAsync(string email);
     Task<AddressResponse?> GetCurrentUserAddressAsync(string email);
     Task<AddressResponse?> UpdateCurrentUserAddressAsync(AddressResponse request,string email);
    }
}
