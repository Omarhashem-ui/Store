using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.ProductsExceptions
{
    public class ProductNotFoundException(int id) : NotFoundExceptions($"Product With id {id} Not Found")
    {
    }
}
