using Core.Context.Entity;

namespace Dal.SqLite.EF
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ShopProductId { get; set; }
        public int Quantity { get; set; }
    }
}
