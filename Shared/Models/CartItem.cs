namespace Shared.Models
{
    public class CartItem
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public Decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public Product Product { get; set; }
    }
}
