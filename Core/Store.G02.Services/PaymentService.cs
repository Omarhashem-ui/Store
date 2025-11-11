using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.BasketExceptions;
using Store.G02.Domain.Exceptions.OrderEceptions;
using Store.G02.Domain.Exceptions.ProductsExceptions;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.BasketDto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.G02.Domain.Entities.Products.Product;

namespace Store.G02.Services
{
    public class PaymentService(IBasketRepository _basketRepository,IUnitOfWork _unitOfWork,IConfiguration configuration,IMapper _mapper) : IPaymentService
    {
        public async Task<CustomerBasketDto?> CreatePaymentIntentAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundExceptions(basketId);
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            var subTotal = basket.Items.Sum(item => item.Price * item.Quantity);
            if (basket.DeliveryMethodId is null) throw new DeliveryMethodNotFoundExcceptions(basket.DeliveryMethodId.Value);
            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundExcceptions(basket.DeliveryMethodId.Value);

            basket.ShippingCoast = deliveryMethod.Price;
            var amount = subTotal + deliveryMethod.Price;
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"]=Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
            PaymentIntentService paymentService = new PaymentIntentService();
            
            if (basket.PaymentIntentId is null)
            {
                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await paymentService.CreateAsync(paymentIntentOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var paymentIntentUpdateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(amount * 100),
                };
                await paymentService.UpdateAsync(basket.PaymentIntentId, paymentIntentUpdateOptions);

            }

           basket= await _basketRepository.CreateBasketAsync(basket,TimeSpan.FromDays(1));
            return _mapper.Map<CustomerBasketDto>(basket);

        }
    }
}
