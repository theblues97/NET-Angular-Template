using System.ComponentModel.DataAnnotations;

namespace Core.Context.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid? TenantId { get; set; }

    }
}
