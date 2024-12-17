using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Exceptions;

namespace SchoolManagerModel.Persistence;

public class ClassDatabase(IDbContextFactory<SchoolDbContext> dbContextFactory) : IAsyncClassDataHandler
{
    public async Task<List<Class>> GetClassesAsync()
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Classes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddClassAsync(Class cls)
    {
        if (await ClassExistsAsync(cls))
        {
            throw new ClassExistsException($"Class with name {cls.Name} already exists.");
        }

        await using var context = await dbContextFactory.CreateDbContextAsync();
        await context.Classes.AddAsync(cls);
        await context.SaveChangesAsync();
    }

    public async Task<List<User>> GetClassStudentsAsync(Class cls)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        var result = await context.Students
            .AsNoTracking()
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .Include(x => x.User)
            .Select(x => x.User)
            .ToListAsync();

        return result
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Id)
            .ToList();
    }

    public async Task<List<Subject>> GetClassSubjectsAsync(Class cls)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Subjects
            .AsNoTracking()
            .Include(x => x.Class)
            .Where(x => x.Class.Id == cls.Id)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> ClassExistsAsync(Class cls)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync();
        return await context.Classes
            .AsNoTracking()
            .AnyAsync(x => x.Year == cls.Year && x.SchoolClass == cls.SchoolClass);
    }
}