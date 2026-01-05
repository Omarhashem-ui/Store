using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.BasketExceptions;
using Store.G02.Domain.Exceptions.OrderEceptions;
using Store.G02.Domain.Exceptions.ProductsExceptions;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Specifications.Orders;
using Store.G02.Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class OrderServices(IUnitOfWork _unitOfWork,IMapper _mapper,IServicesManager _servicesManager) : IOrderServices
    {
       
        public async Task<OrderResponse> GetOrderAsync(OrderRequest request, string userEmail)
        {
            var shippingAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

           var deliverMethod =await _servicesManager.DeliveryMethodService.GetMethodByIdAsync(request.DeliverMethodId);
            var (basket,orderItems) =await _servicesManager.BasketOrderService.GetOrderItemsAsync(request);
            var subTotal = orderItems.Sum(OI=>OI.Price*OI.Quantity);
            var spec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);
            var existOrder = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            if (existOrder is not null)
            {
                _unitOfWork.GetRepository<Guid, Order>().Delete(existOrder);
               
            }

            var order = new Order(userEmail, shippingAddress, deliverMethod,orderItems,subTotal,basket.PaymentIntentId);
           await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
           var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new OrderBadRequestExceptions();
            return _mapper.Map<OrderResponse>(order);
        }
        public async Task<DeliveryMethodResponse> GetAllDeliveryMethodAsync()
        {
         var deliveryMethod= await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
           
            return _mapper.Map<DeliveryMethodResponse>(deliveryMethod);
        }

        

        public async Task<OrderResponse> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecification(id, userEmail);
           var order =await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundExceptions(id);
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {
            var spec =new OrderSpecification(userEmail);
          var order= await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResponse>>(order);
        }

        public async Task UpdateOrderStatusAsync(string paymentIntentId, OrderStatus newStatus)
        {
            var spec = new OrderWithPaymentIntentSpecification(paymentIntentId);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);

            if (order is not null)
            {
                order.Status = newStatus;
                await _unitOfWork.SaveChangesAsync();
            }
        }

    }
}
