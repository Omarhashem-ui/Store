using Store.G02.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IDeliveryMethodService
    {
        Task<DeliveryMethod> GetMethodByIdAsync(int id);
    }
}
