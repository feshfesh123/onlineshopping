namespace OnlineShopping.Data.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quanity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}