using Store.G02.Domain.Entities.Orders;
using Store.G02.Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IOrderServices
    {
      Task<OrderResponse>  GetOrderAsync(OrderRequest request, string userEmail);
        Task<DeliveryMethodResponse>GetAllDeliveryMethodAsync();
        Task<OrderResponse>GetOrderByIdForSpecificUserAsync(Guid id , string userEmail);
        Task<IEnumerable<OrderResponse>>GetOrdersForSpecificUserAsync( string userEmail);
        Task UpdateOrderStatusAsync(string paymentIntentId, OrderStatus newStatus);

    }
}
