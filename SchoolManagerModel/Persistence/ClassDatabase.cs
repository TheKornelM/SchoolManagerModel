using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

public class ClassDatabase(SchoolDbContextBase dbContext) : IAsyncClassDataHandler
{
    private readonly SchoolDbContextBase _dbContext = dbContext;

    public async Task<List<Class>> GetClassesAsync()
    {
        return await _dbContext.Classes.ToListAsync();
    }

    public async Task AddClassAsync(Class cls)
    {
        await _dbContext.Classes.AddAsync(cls);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetClassStudentsAsync(Class cls)
    {
        return await _dbContext.Students
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .Include(x => x.User)
            .Select(x => x.User)
            .ToListAsync();
    }

    public async Task<List<Subject>> GetClassSubjectsAsync(Class cls)
    {
        return await _dbContext.Subjects
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .ToListAsync();
    }

    public async Task<bool> ClassExistsAsync(Class cls)
    {
        return await _dbContext.Classes
            .AnyAsync(x => x.Name == cls.Name);
    }
}