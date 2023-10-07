using Core.Context.Entity;

namespace Domain.Entity
{
    public class ShopProduct : BaseEntity
    {
        public int ShopId { get; set; }
        public int ProductId { get; set; }
    }
}
