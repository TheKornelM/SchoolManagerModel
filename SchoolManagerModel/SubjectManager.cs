using SchoolManagerModel.Persistence;
using SchoolManagerModel.UserModel;

namespace SchoolManagerModel;

public class SubjectManager(IAsyncSubjectDataHandler dataHandler)
{
    readonly IAsyncSubjectDataHandler _dataHandler = dataHandler;
    public async Task AddSubjectMarkAsync(Student student, Subject subject, Mark mark)
    {
        await CheckAssignedSubjectToStudentAsync(student, subject);
        await _dataHandler.AddMarkAsync(mark);
    }

    public async Task<List<Mark>> GetStudentSubjectMarksAsync(Student student, Subject subject)
    {
        await CheckAssignedSubjectToStudentAsync(student, subject);
        return await _dataHandler.GetStudentSubjectMarksAsync(student, subject);
    }


    private async Task CheckAssignedSubjectToStudentAsync(Student student, Subject subject)
    {
        if (!await _dataHandler.IsAssignedSubjectToStudentAsync(student.User, subject))
        {
            throw new Exception("Subject is not assigned to student");
        }
    }

    public async Task AddSubjectAsync(Subject subject)
    {
        await _dataHandler.AddSubjectAsync(subject);
    }

    public async Task AssignSubjectsToStudentAsync(Student student, List<Subject> subjects)
    {
        await _dataHandler.AssignSubjectsToStudentAsync(student, subjects);
    }

    public async Task<List<Student>> GetSubjectStudentsAsync(Subject subject)
    {
        return await _dataHandler.GetSubjectStudentsAsync(subject);
    }

    public async Task<List<Mark>> GetStudentMarkAsync(Student student)
    {
        return await _dataHandler.GetStudentMarksAsync(student);
    }
}
