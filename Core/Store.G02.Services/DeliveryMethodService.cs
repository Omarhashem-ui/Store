using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Exceptions.OrderEceptions;
using Store.G02.Services.Abstractions;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class DeliveryMethodService (IUnitOfWork _unitOfWork): IDeliveryMethodService
    {
        public async Task<DeliveryMethod> GetMethodByIdAsync(int id)
        {
            var deliverMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(id);
            if (deliverMethod is null) throw new DeliveryMethodNotFoundExcceptions(id);
            return deliverMethod;
        }
    }
}
