using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.OrderEceptions
{
    public class DeliveryMethodNotFoundExcceptions (int id) : NotFoundExceptions($"Delivery Method With Id {id} is Not Found")
    {
    }
}
