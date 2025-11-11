using AutoMapper;
using Store.G02.Domain.Entities.Basket;
using Store.G02.Shared.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, ItemBasketDto>().ReverseMap();




        }
    }
}
