using Moq;
using SchoolManagerModel;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.UserModel;

namespace SchoolManager.Tests
{
    [TestClass]
    public class ClassManagerTests
    {
        private Mock<IAsyncClassDataHandler> _mockDataHandler = new Mock<IAsyncClassDataHandler>();
        private ClassManager _classManager;

        private Class _testClass = new Class { Id = 1, Name = "1/A" };
        private User _testStudent = new User("username", "password", "email", "firstName", "lastName");
        private Subject _testSubject;

        public ClassManagerTests()
        {
            // Mock the data handler dependency
            _classManager = new ClassManager(_mockDataHandler.Object);

            // Initialize test data
            _testSubject = new Subject
            {
                Id = 1,
                Name = "Math",
                Class = _testClass,
                Teacher = new Teacher { User = new User("teacher", "pass", "email", "firstName", "lastName") }
            };
        }

        public void Setup()
        {

        }

        [TestMethod]
        public async Task GetClassesAsync_ShouldReturnClasses()
        {
            // Arrange
            var classes = new List<Class> { _testClass };
            _mockDataHandler.Setup(dh => dh.GetClassesAsync()).ReturnsAsync(classes);

            // Act
            var result = await _classManager.GetClassesAsync();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_testClass, result[0]);
            _mockDataHandler.Verify(dh => dh.GetClassesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddClassAsync_ShouldAddClass_WhenClassDoesNotExist()
        {
            // Arrange
            _mockDataHandler.Setup(dh => dh.ClassExistsAsync(_testClass)).ReturnsAsync(false);

            // Act
            await _classManager.AddClassAsync(_testClass);

            // Assert
            _mockDataHandler.Verify(dh => dh.AddClassAsync(_testClass), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ClassExistsException))]
        public async Task AddClassAsync_ShouldThrowException_WhenClassExists()
        {
            // Arrange
            _mockDataHandler.Setup(dh => dh.ClassExistsAsync(_testClass)).ReturnsAsync(true);

            // Act
            await _classManager.AddClassAsync(_testClass);

            // Assert - Expect exception
        }

        [TestMethod]
        public async Task GetClassStudentsAsync_ShouldReturnClassStudents()
        {
            // Arrange
            var students = new List<User> { _testStudent };
            _mockDataHandler.Setup(dh => dh.GetClassStudentsAsync(_testClass)).ReturnsAsync(students);

            // Act
            var result = await _classManager.GetClassStudentsAsync(_testClass);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_testStudent, result[0]);
            _mockDataHandler.Verify(dh => dh.GetClassStudentsAsync(_testClass), Times.Once);
        }

        [TestMethod]
        public async Task GetClassSubjectsAsync_ShouldReturnClassSubjects()
        {
            // Arrange
            var subjects = new List<Subject> { _testSubject };
            _mockDataHandler.Setup(dh => dh.GetClassSubjectsAsync(_testClass)).ReturnsAsync(subjects);

            // Act
            var result = await _classManager.GetClassSubjectsAsync(_testClass);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_testSubject, result[0]);
            _mockDataHandler.Verify(dh => dh.GetClassSubjectsAsync(_testClass), Times.Once);
        }
    }
}
