using Core.Context.Entity;

namespace Dal.SqLite.EF
{
    public class ShopProduct : BaseEntity
    {
        public int ShopId { get; set; }
        public int ProductId { get; set; }
    }
}
