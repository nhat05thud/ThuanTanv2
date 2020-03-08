namespace ThuanTanUmbraco.Models
{
    public class ProductCartModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int CartId { get; set; }
    }
}