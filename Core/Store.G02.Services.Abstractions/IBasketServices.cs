using Store.G02.Shared.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IBasketServices
    {
      Task<CustomerBasketDto?> GetBasketByIdAsync(string id);
      Task<CustomerBasketDto?> CreateOrUpdateBaketAsync(CustomerBasketDto dto, TimeSpan duration);
      Task DeleteBasketAsync(string id);
    }
}
