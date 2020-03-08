namespace ThuanTan.Entity.Models
{
    public class ProductCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
        public Cart Cart { get; set; }
    }
}
