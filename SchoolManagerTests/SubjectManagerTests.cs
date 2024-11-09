using Moq;
using SchoolManagerModel;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.UserModel;

namespace SchoolManager.Tests
{
    [TestClass]
    public class SubjectManagerTests
    {
        private Mock<IAsyncSubjectDataHandler> _mockDataHandler = new Mock<IAsyncSubjectDataHandler>();
        private SubjectManager _subjectManager;
        private Student _testStudent;
        private Subject _testSubject;
        private Mark _testMark;

        public SubjectManagerTests()
        {
            _subjectManager = new SubjectManager(_mockDataHandler.Object);

            var testClass = new Class { Id = 1, Name = "1/A" };
            var testUser = new User("teacherUsername", "password", "teacherEmail", "teacherFirstName", "teacherLastName");

            // Setup test entities with required fields
            _testStudent = new Student
            {
                Id = 1,
                User = new User("username", "password", "email", "firstName", "lastName"),
                Class = testClass
            };

            _testSubject = new Subject
            {
                Id = 1,
                Name = "Math",
                Class = testClass,
                Teacher = new Teacher { User = testUser }
            };

            _testMark = new Mark
            {
                Id = 1,
                Grade = 90,
                Student = _testStudent,
                Subject = _testSubject,
                SubmitDate = DateTime.Now
            };
        }

        [TestMethod]
        public async Task AddSubjectMarkAsync_ShouldAddMark_WhenSubjectAssignedToStudent()
        {
            _mockDataHandler.Setup(m => m.IsAssignedSubjectToStudentAsync(_testStudent.User, _testSubject)).ReturnsAsync(true);
            _mockDataHandler.Setup(m => m.AddMarkAsync(_testMark)).Returns(Task.CompletedTask);

            await _subjectManager.AddSubjectMarkAsync(_testStudent, _testSubject, _testMark);

            _mockDataHandler.Verify(m => m.IsAssignedSubjectToStudentAsync(_testStudent.User, _testSubject), Times.Once);
            _mockDataHandler.Verify(m => m.AddMarkAsync(_testMark), Times.Once);
        }

        [TestMethod]
        public async Task AddSubjectMarkAsync_ShouldThrowException_WhenSubjectNotAssignedToStudent()
        {
            _mockDataHandler.Setup(m => m.IsAssignedSubjectToStudentAsync(_testStudent.User, _testSubject)).ReturnsAsync(false);

            await Assert.ThrowsExceptionAsync<Exception>(() => _subjectManager.AddSubjectMarkAsync(_testStudent, _testSubject, _testMark));
            _mockDataHandler.Verify(m => m.AddMarkAsync(It.IsAny<Mark>()), Times.Never);
        }

        [TestMethod]
        public async Task GetStudentSubjectMarksAsync_ShouldReturnMarks_WhenSubjectAssignedToStudent()
        {
            var marks = new List<Mark> { _testMark };
            _mockDataHandler.Setup(m => m.IsAssignedSubjectToStudentAsync(_testStudent.User, _testSubject)).ReturnsAsync(true);
            _mockDataHandler.Setup(m => m.GetStudentSubjectMarksAsync(_testStudent, _testSubject)).ReturnsAsync(marks);

            var result = await _subjectManager.GetStudentSubjectMarksAsync(_testStudent, _testSubject);

            Assert.AreEqual(marks, result);
            _mockDataHandler.Verify(m => m.GetStudentSubjectMarksAsync(_testStudent, _testSubject), Times.Once);
        }

        [TestMethod]
        public async Task AssignSubjectsToStudentAsync_ShouldCallAssignSubjectsToStudent()
        {
            var subjects = new List<Subject> { _testSubject };
            _mockDataHandler.Setup(m => m.AssignSubjectsToStudentAsync(_testStudent, subjects)).Returns(Task.CompletedTask);

            await _subjectManager.AssignSubjectsToStudentAsync(_testStudent, subjects);

            _mockDataHandler.Verify(m => m.AssignSubjectsToStudentAsync(_testStudent, subjects), Times.Once);
        }

        [TestMethod]
        public async Task GetSubjectStudentsAsync_ShouldReturnStudents()
        {
            var students = new List<Student> { _testStudent };
            _mockDataHandler.Setup(m => m.GetSubjectStudentsAsync(_testSubject)).ReturnsAsync(students);

            var result = await _subjectManager.GetSubjectStudentsAsync(_testSubject);

            CollectionAssert.AreEqual(students, result);
            _mockDataHandler.Verify(m => m.GetSubjectStudentsAsync(_testSubject), Times.Once);
        }

        [TestMethod]
        public async Task GetStudentMarkAsync_ShouldReturnStudentMarks()
        {
            var marks = new List<Mark> { _testMark };
            _mockDataHandler.Setup(m => m.GetStudentMarksAsync(_testStudent)).ReturnsAsync(marks);

            var result = await _subjectManager.GetStudentMarkAsync(_testStudent);

            CollectionAssert.AreEqual(marks, result);
            _mockDataHandler.Verify(m => m.GetStudentMarksAsync(_testStudent), Times.Once);
        }
    }
}
