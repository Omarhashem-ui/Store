using Store.G02.Services.Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IServicesManager
    {
         IProductServices ProductServices { get;}
         IBasketServices BasketServices { get;} 
         ICacheServices CacheServices { get;} 
         IAuthServices AuthServices { get;} 
         IOrderServices OrderServices { get;} 
         IPaymentService PaymentService { get;} 
         IBasketOrderService BasketOrderService { get; }
        IDeliveryMethodService DeliveryMethodService { get; }
    }
}
