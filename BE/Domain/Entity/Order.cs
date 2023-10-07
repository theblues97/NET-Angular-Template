using Core.Context.Entity;

namespace Domain.Entity
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ShopProductId { get; set; }
        public int Quantity { get; set; }
    }
}
