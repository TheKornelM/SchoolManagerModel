using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public class UserDatabase(SchoolDbContextBase dbContext) : IAsyncUserDataHandler
{
    private readonly SchoolDbContextBase _dbContext = dbContext;

    public async Task<User?> GetUserAsync(string username)
    {
        await _dbContext.Users.LoadAsync();
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task<Role?> GetRoleAsync(User user)
    {
        var roleRecord = await _dbContext.Roles
            .FirstOrDefaultAsync(currentUser => currentUser.UserId == user.Id);

        return roleRecord != null ? (Role?)roleRecord.RoleId : null;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _dbContext.Users.AnyAsync(user => user.Username == username);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(currentUser => currentUser.Username == username);
    }

    public async Task<bool> EmailAlreadyRegisteredAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task AssignRoleAsync(User user, Role role)
    {
        // The student role is the default, so it is not stored
        if (role == Role.Student)
            return;

        await _dbContext.Roles.AddAsync(new RoleRecord(user.Id, (int)role));
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task AddStudentAsync(Student student)
    {
        _dbContext.Attach(student.Class);
        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddTeacherAsync(Teacher teacher)
    {
        await _dbContext.Teachers.AddAsync(teacher);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAdminAsync(Admin admin)
    {
        await _dbContext.Admins.AddAsync(admin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Student?> GetStudentByUserAsync(User user)
    {
        return await _dbContext.Students
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.User.Id == user.Id);
    }
}