using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Products;
using Store.G02.Shared.AuthDto;

namespace Store.G02.Services
{
    public class ServicesManager : IServicesManager
    {
        public IProductServices ProductServices { get; }
        public IBasketServices BasketServices { get; }
        public ICacheServices CacheServices { get; }
        public IAuthServices AuthServices { get; }
        public IOrderServices OrderServices { get; }
        public IPaymentService PaymentService { get; }
        public IBasketOrderService BasketOrderService { get; }
        public IDeliveryMethodService DeliveryMethodService { get; }

        
        public ServicesManager(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheRepository cacheRepository,
            UserManager<AppUser> userManager,
            IConfiguration configuration,
            IOptions<JwtOptions> options)
        {
     
            BasketOrderService = new BasketOrderService(unitOfWork, basketRepository);
            DeliveryMethodService = new DeliveryMethodService(unitOfWork);
            ProductServices = new ProductServices(unitOfWork, mapper);
            BasketServices = new BasketServices(basketRepository, mapper);
            CacheServices = new CacheServices(cacheRepository);
            AuthServices = new AuthService(userManager, options, mapper);
            OrderServices = new OrderServices(unitOfWork, mapper, this);
            PaymentService = new PaymentService(basketRepository, unitOfWork, configuration, mapper);
        }
    }
}
