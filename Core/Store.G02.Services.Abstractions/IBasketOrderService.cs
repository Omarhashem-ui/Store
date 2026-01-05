using Store.G02.Domain.Entities.Basket;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IBasketOrderService
    {
        Task<(CustomerBasket basket,List<OrderItem> orderItems)> GetOrderItemsAsync(OrderRequest request);
    }
}
