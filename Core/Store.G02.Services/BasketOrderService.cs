using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Basket;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.BasketExceptions;
using Store.G02.Domain.Exceptions.ProductsExceptions;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.OrderDto;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class BasketOrderService(IUnitOfWork _unitOfWork, IBasketRepository _basketRepository) : IBasketOrderService
    {
        
        public async Task<(CustomerBasket basket ,List<OrderItem> orderItems)> GetOrderItemsAsync(OrderRequest request) 
        {
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundExceptions(request.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);
                if (product.Price != item.Price) item.Price = product.Price;
                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            return (basket,orderItems);
        }
      
    }
}
