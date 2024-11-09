using Microsoft.EntityFrameworkCore;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Persistence;

internal class SubjectDatabase : IAsyncSubjectDataHandler
{
    readonly SchoolDbContext _dbContext = new();
    public async Task<List<Mark>> GetStudentSubjectMarksAsync(Student student, Subject subject)
    {
        return await _dbContext.Marks
            .Where(x => x.Subject == subject && x.Student == student)
            .ToListAsync();
    }

    public async Task<bool> IsAssignedSubjectToStudentAsync(User student, Subject subject)
    {
        return await _dbContext.AssignedSubjects
            .Include(x => x.Student.User)
            .AnyAsync(x => x.Student.User.Id == student.Id && x.Subject.Id == subject.Id);
    }

    public async Task AddMarkAsync(Mark mark)
    {
        _dbContext.Attach(mark);
        await _dbContext.Marks.AddAsync(mark);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddSubjectAsync(Subject subject)
    {
        _dbContext.Attach(subject);
        await _dbContext.Subjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AssignSubjectsToStudentAsync(Student student, List<Subject> subjects)
    {

        var dbStudent = _dbContext.Students
            .Include(x => x.Subjects)
            .FirstOrDefault(x => x.Id == student.Id);

        if (dbStudent == null)
        {
            throw new Exception("Student not found");
        }

        dbStudent.Subjects = dbStudent.Subjects ?? [];

        foreach (var item in subjects)
        {
            dbStudent.Subjects.Add(new AssignedSubject
            {
                Subject = item,
                Student = student
            });
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Student>> GetSubjectStudentsAsync(Subject subject)
    {
        var students = await _dbContext.AssignedSubjects
            .Include(x => x.Student)
            .ThenInclude(x => x.User)
            .Where(x => x.Subject == subject)
            .Select(x => x.Student)
            .ToListAsync();

        return students;

    }

    public async Task<List<Mark>> GetStudentMarksAsync(Student student)
    {
        return await _dbContext.Marks
            .Include(x => x.Subject).ThenInclude(x => x.Teacher)
            .Where(x => x.Student == student)
            .ToListAsync();
    }
}