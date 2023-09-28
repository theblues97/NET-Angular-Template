using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Dal.InMemory.EF;

namespace Dal.InMemory.Context;

public partial class InMemoryContext : DbContext
{

    public InMemoryContext(DbContextOptions<InMemoryContext>
        options) : base(options)
    {

    }

    public virtual DbSet<Employee> HrMasEmployees { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(new List<Employee>()
        {
            new()
            {
                Id = 1,
                Name = "Test",
                Gender = 1
            }
        });
    }
}