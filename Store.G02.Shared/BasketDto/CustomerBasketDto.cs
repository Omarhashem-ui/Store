using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.BasketDto
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public IEnumerable<ItemBasketDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCoast { get; set; }
    }
}
