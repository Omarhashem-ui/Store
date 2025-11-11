using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.BasketExceptions
{
    public class DeleteBadRequestExceptions(string id) : BadRequestExceptions($"Inavlid Opertion when Delete Basket With Id {id}")
    {
    }
}
