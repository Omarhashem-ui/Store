using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Products;
using Store.G02.Shared.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class ServicesManager
        (IBasketRepository _basketRepository
        ,IUnitOfWork _unitOfWork,
        IMapper _mapper,
        ICacheRepository _cacheRepository,
        UserManager<AppUser> _userManager,
        IConfiguration configuration,
        IOptions<JwtOptions> _options): IServicesManager
    {
        public IProductServices ProductServices { get; }=new ProductServices(_unitOfWork,_mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(_basketRepository,_mapper);
        public ICacheServices CacheServices { get; } = new CacheServices(_cacheRepository);

        public IAuthServices AuthServices { get; } = new AuthService(_userManager,_options,_mapper);

        public IOrderServices orderServices { get; } = new OrderServices(_unitOfWork, _mapper,_basketRepository);

        public IPaymentService PaymentService { get; } = new PaymentService(_basketRepository,_unitOfWork,configuration,_mapper);
    }
}
