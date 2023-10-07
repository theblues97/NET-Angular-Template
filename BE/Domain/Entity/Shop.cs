using Core.Context.Entity;

namespace Domain.Entity
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
    }
}
