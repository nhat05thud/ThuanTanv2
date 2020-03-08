namespace ThuanTanUmbraco.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Url { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}