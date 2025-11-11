using Store.G02.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Specifications.Orders
{
    public class OrderWithPaymentIntentSpecification : BaseSpecifications<Guid,Order>
    {
        public OrderWithPaymentIntentSpecification(string PaymentIntentId):base(O=>O.PaymentIntentId==PaymentIntentId)
        {
            
        }
    }
}
