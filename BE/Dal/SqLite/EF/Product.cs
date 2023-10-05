using Core.Context.Entity;

namespace Dal.SqLite.EF
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public double Price { get; set; }

    }
}
