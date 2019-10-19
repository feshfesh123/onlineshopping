namespace OnlineShopping.Data.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quanity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}