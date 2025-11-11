using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.OrderDto
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddressDto ShippingAdddress { get; set; }
        public string DeliveryMethod { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
