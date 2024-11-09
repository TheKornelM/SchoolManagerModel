using SchoolManagerModel.Persistence;
using SchoolManagerModel.UserModel;

namespace SchoolManagerModel;

public class ClassManager(IAsyncClassDataHandler dataHandler)
{
    private readonly IAsyncClassDataHandler _dataHandler = dataHandler;

    public async Task<List<Class>> GetClassesAsync()
    {
        return await _dataHandler.GetClassesAsync();
    }

    public async Task AddClassAsync(Class cls)
    {
        if (await _dataHandler.ClassExistsAsync(cls))
        {
            throw new ClassExistsException($"{cls.Name} class exists");
        }

        await _dataHandler.AddClassAsync(cls);
    }

    public async Task<List<User>> GetClassStudentsAsync(Class cls)
    {
        return await _dataHandler.GetClassStudentsAsync(cls);
    }

    public async Task<List<Subject>> GetClassSubjectsAsync(Class cls)
    {
        return await _dataHandler.GetClassSubjectsAsync(cls);
    }

}
