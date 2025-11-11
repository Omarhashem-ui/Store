using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.BasketExceptions
{
    public class BasketNotFoundExceptions(string id) : NotFoundExceptions($"Basket With Id{id} Is Not Found")
    {
    }
}
