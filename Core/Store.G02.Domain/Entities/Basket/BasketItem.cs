namespace Store.G02.Domain.Entities.Basket
{
    public class BasketItem : BaseEntity<int>
    {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}