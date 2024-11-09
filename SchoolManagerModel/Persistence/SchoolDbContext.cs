using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Utils;

namespace SchoolManagerModel.Persistence;

public class SchoolDbContext : DbContext
{
    public DbSet<RoleRecord> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<AssignedSubject> AssignedSubjects { get; set; }
    public DbSet<Mark> Marks { get; set; }

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