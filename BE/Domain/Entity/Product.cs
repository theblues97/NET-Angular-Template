using Core.Context.Entity;

namespace Domain.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public double Price { get; set; }

    }
}
