using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress shippingAdddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string? paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAdddress = shippingAdddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAdddress { get; set; } 
        public DeliveryMethod DeliveryMethod{ get; set; }
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> Items  { get; set; }
        public decimal SubTotal { get; set; }


        public decimal GetTotal() => SubTotal+DeliveryMethod.Price;
        public string? PaymentIntentId { get; set; }
    }
}
