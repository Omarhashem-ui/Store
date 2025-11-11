using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Domain.Exceptions;
using Store.G02.Domain.Exceptions.AuthExceptions;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.AuthDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class AuthService(UserManager<AppUser> _userManager,IOptions<JwtOptions> _options,IMapper _mapper): IAuthServices
    {
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
           return await _userManager.FindByEmailAsync(email) != null;
        }
        public async Task<UserResponse?> GetCurrentUserAsync(string email)
        {
          var user= await _userManager.FindByEmailAsync(email);
            if (user is null) throw new UserNotFoundException(email);
            return _mapper.Map<UserResponse>(user);

        }
        public async Task<AddressResponse?> GetCurrentUserAddressAsync(string email)
        {
          var user= await _userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);
            return _mapper.Map<AddressResponse>(user.Address);
        }

        public async Task<AddressResponse?> UpdateCurrentUserAddressAsync(AddressResponse request, string email)
        {
           var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
           if(user is null) throw new UserNotFoundException(email);
           if(user.Address is null)
            {
                user.Address = _mapper.Map<Addresses>(request);
            }
            else
            {
                user.Address.FirstName= request.FirstName;
                user.Address.LastName= request.LastName;
                user.Address.Street= request.Street;
                user.Address.City= request.City;
                user.Address.Country= request.Country;

            }
           await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressResponse>(user.Address);
        }

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)throw new UserNotFoundException(request.Email);
           var flag=  await _userManager.CheckPasswordAsync(user, request.Password);
            if (!flag) throw new UnauthorizedExceptions();
            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateToken(user)
            };
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName= request.UserName,
                Email=request.Email,
                DisplayName= request.DisplayName,
                PhoneNumber= request.PhoneNumber
            };
         var result =  await _userManager.CreateAsync(user,request.Password);
            if (!result.Succeeded) throw new RegistrationBadRequestExceptions(result.Errors.Select(E => E.Description).ToList());
            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateToken(user)
            };
        }

      

        private async Task<string> GenerateToken(AppUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var jwtOption = _options.Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key));
            
            var token = new JwtSecurityToken 
                (
                issuer:jwtOption.Issuer,
                audience: jwtOption.Audience,
                claims:authClaims,
                expires: DateTime.Now.AddDays(double.Parse(jwtOption.ExpireDate)),
                signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
