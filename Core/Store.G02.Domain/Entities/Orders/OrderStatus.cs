using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Entities.Orders
{
    public enum OrderStatus
    {
        Pending = 0,
        PaymentSucceses = 1,
        PaymentFailed = 2,
    }
}
