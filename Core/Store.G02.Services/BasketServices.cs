using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Basket;
using Store.G02.Domain.Exceptions.BasketExceptions;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class BasketServices(IBasketRepository _basketRepository,IMapper _mapper) : IBasketServices
    {
        public async Task<CustomerBasketDto?> GetBasketByIdAsync(string id)
        {
             var result= await _basketRepository.GetBasketAsync(id);
            if (result == null) throw new BasketNotFoundExceptions(id);
          var dto =  _mapper.Map<CustomerBasketDto>(result);
            return dto;
        }
        public async Task<CustomerBasketDto?> CreateOrUpdateBaketAsync(CustomerBasketDto dto, TimeSpan duration)
        {
           var basket = _mapper.Map<CustomerBasket>(dto);
         var result = await _basketRepository.CreateBasketAsync(basket, duration);
            if (result == null) throw new CreateOrUpdateBadRequestExceptions();
            return _mapper.Map<CustomerBasketDto>(result);

        }

        public async Task DeleteBasketAsync(string id)
        {
         var flag=  await _basketRepository.DeleteBasketAsync(id);
            if (!flag) throw new DeleteBadRequestExceptions(id);
        }

       
    }
}
