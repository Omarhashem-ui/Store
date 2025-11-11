namespace Store.G02.Domain.Entities.Orders
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Descraption { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}