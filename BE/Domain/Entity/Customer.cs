using Core.Context.Entity;

namespace Domain.Entity
{
    public partial class Customer : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string Email { get; set; } = null!;
    }
}
