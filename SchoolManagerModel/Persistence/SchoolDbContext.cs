using Microsoft.EntityFrameworkCore;

namespace SchoolManagerModel.Persistence;

public class SchoolDbContext(DbContextOptions optionsBuilder) : SchoolDbContextBase(optionsBuilder)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // var seeder = new EntityDataSeeder(modelBuilder);
        // seeder.SeedAllData();
    }
}