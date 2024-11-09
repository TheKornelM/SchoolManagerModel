using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Utils;

namespace SchoolManagerModel.Persistence;

public class SchoolDbContext : SchoolDbContextBase
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=school.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Change password after first run!
        var admin = new User("admin", HashStringMd5.GetHashedString("admin"), "admin@test.localhost", "firstName",
            "secondName")
        {
            Id = 1
        };
        modelBuilder.Entity<User>().HasData(admin);

        // Assign administrator role to default account
        var adminRole = new RoleRecord(1, 2)
        {
            Id = 1
        };
        modelBuilder.Entity<RoleRecord>().HasData(adminRole);
    }
}