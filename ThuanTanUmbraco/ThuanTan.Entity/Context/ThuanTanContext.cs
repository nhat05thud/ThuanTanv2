using System.Data.Entity;
using ThuanTan.Entity.Models;

namespace ThuanTan.Entity.Context
{
    public class ThuanTanContext : DbContext
    {
        public ThuanTanContext()
            : base("umbracoDbDSN")
        {
        }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<ProductCart> ProductCarts { get; set; }
    }
}
