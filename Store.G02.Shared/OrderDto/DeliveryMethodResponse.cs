using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.OrderDto
{
    public class DeliveryMethodResponse
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Descraption { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}
