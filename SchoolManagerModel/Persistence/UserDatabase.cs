﻿using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public class UserDatabase(SchoolDbContextBase dbContext) : IAsyncUserDataHandler
{
    public async Task<User?> GetUserAsync(string username)
    {
        await dbContext.Users.LoadAsync();
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task<Role?> GetRoleAsync(User user)
    {
        var roleRecord = await dbContext.Roles
            .FirstOrDefaultAsync(currentUser => currentUser.UserId == user.Id);

        return roleRecord != null ? (Role?)roleRecord.RoleId : null;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await dbContext.Users.AnyAsync(user => user.Username == username);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(currentUser => currentUser.Username == username);
    }

    public async Task<bool> EmailAlreadyRegisteredAsync(string email)
    {
        return await dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task AssignRoleAsync(User user, Role role)
    {
        // The student role is the default, so it is not stored
        if (role == Role.Student)
            return;

        await dbContext.Roles.AddAsync(new RoleRecord(user.Id, (int)role));
        await dbContext.SaveChangesAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task AddStudentAsync(Student student)
    {
        dbContext.Attach(student.Class);
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddTeacherAsync(Teacher teacher)
    {
        await dbContext.Teachers.AddAsync(teacher);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddAdminAsync(Admin admin)
    {
        await dbContext.Admins.AddAsync(admin);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Student?> GetStudentByUserAsync(User user)
    {
        return await dbContext.Students
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.User.Id == user.Id);
    }
}