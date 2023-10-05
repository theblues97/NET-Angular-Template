using Core.Context.Entity;

namespace Dal.SqLite.EF
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
    }
}
