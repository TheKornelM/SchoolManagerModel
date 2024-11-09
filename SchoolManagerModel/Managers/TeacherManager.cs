using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Persistence;

namespace SchoolManagerModel.Managers;

public class TeacherManager(IAsyncTeacherDataHandler dataHandler)
{
    private readonly IAsyncTeacherDataHandler _dataHandler = dataHandler;

    public async Task<List<Teacher>> GetTeacherUsersAsync()
    {
        return await _dataHandler.GetTeachersAsync();
    }

    public async Task<List<Subject>> GetCurrentTaughtSubjectsAsync(Teacher teacher)
    {
        return await _dataHandler.GetCurrentTaughtSubjectsAsync(teacher);
    }
}
