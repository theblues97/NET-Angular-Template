using Core.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public abstract class BaseDbContext<TContext> : DbContext where TContext : DbContext
    {
        public ICurrentTenant CurrentTenant { get; set; }
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
