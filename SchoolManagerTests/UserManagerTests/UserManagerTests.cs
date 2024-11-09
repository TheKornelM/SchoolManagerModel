using Moq;
using SchoolManagerModel;
using SchoolManagerModel.UserModel;
using SchoolManagerModel.Persistence;

namespace SchoolManagerTests.UserManagerTests;

[TestClass]
public partial class UserManagerTests
{
    readonly Mock<IAsyncUserDataHandler> _handler;
    readonly UserManager _userManager;

    private User _testUser;
    private Student _testStudent;
    private Teacher _testTeacher;
    private Admin _testAdmin;

    public UserManagerTests()
    {
        _handler = new Mock<IAsyncUserDataHandler>();
        _userManager = new UserManager(_handler.Object);
        _testUser = new User("username", "password", "email@test.com", "firstName", "lastName");
        _testStudent = new Student { Id = 1, User = _testUser, Class = new Class() };
        _testTeacher = new Teacher { Id = 2, User = new User("teacher", "password", "teacher@test.com", "firstName", "lastName") };
        _testAdmin = new Admin { Id = 3, User = new User("admin", "password", "admin@test.com", "firstName", "lastName") };
    }



    [TestMethod]
    public async Task IsAdminAsync_ShouldReturnTrue_WhenUserIsAdmin()
    {
        _handler.Setup(dh => dh.GetRoleAsync(_testUser)).ReturnsAsync(Role.Administrator);

        var result = await _userManager.IsAdminAsync(_testUser);
        Assert.IsTrue(result);
        _handler.Verify(dh => dh.GetRoleAsync(_testUser), Times.Once);
    }

    [TestMethod]
    public async Task IsAdminAsync_ShouldReturnFalse_WhenUserIsNotAdmin()
    {
        _handler.Setup(dh => dh.GetRoleAsync(_testUser)).ReturnsAsync(Role.Student);

        var result = await _userManager.IsAdminAsync(_testUser);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task GetRoleAsync_ShouldReturnStudent_WhenRoleIsNull()
    {
        _handler.Setup(dh => dh.GetRoleAsync(_testUser)).ReturnsAsync((Role?)null);

        var result = await _userManager.GetRoleAsync(_testUser);
        Assert.AreEqual(Role.Student, result);
    }

    [TestMethod]
    public async Task UserExistsAsync_ShouldReturnTrue_WhenUserExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);

        var result = await _userManager.UserExistsAsync(_testUser);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task RegisterUserAsync_ShouldThrowException_WhenUserAlreadyExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.RegisterUserAsync(_testUser));
    }

    [TestMethod]
    public async Task RegisterUserAsync_ShouldAddUser_WhenUserIsNew()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(false);
        _handler.Setup(dh => dh.EmailAlreadyRegisteredAsync(_testUser.Email)).ReturnsAsync(false);

        await _userManager.RegisterUserAsync(_testUser);
        _handler.Verify(dh => dh.AddUserAsync(It.Is<User>(u => u.Username == _testUser.Username && u.Password != "password")), Times.Once);
    }

    [TestMethod]
    public async Task AssignRoleAsync_ShouldThrowException_WhenUserNotFound()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(false);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.AssignRoleAsync(_testUser, Role.Teacher));
    }

    [TestMethod]
    public async Task AssignRoleAsync_ShouldAssignRole_WhenUserExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);

        await _userManager.AssignRoleAsync(_testUser, Role.Teacher);
        _handler.Verify(dh => dh.AssignRoleAsync(_testUser, Role.Teacher), Times.Once);
    }

    [TestMethod]
    public async Task GetUserByUsername_ShouldThrowException_WhenUserNotFound()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(false);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.GetUserByUsernameAsync(_testUser.Username));
    }

    [TestMethod]
    public async Task GetUserByUsername_ShouldReturnUser_WhenUserExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);
        _handler.Setup(dh => dh.GetUserByUsernameAsync(_testUser.Username)).ReturnsAsync(_testUser);

        var result = await _userManager.GetUserByUsernameAsync(_testUser.Username);
        Assert.AreEqual(_testUser, result);
    }

    [TestMethod]
    public async Task GetUsersAsync_ShouldReturnAllUsers()
    {
        var users = new List<User> { _testUser, _testTeacher.User, _testAdmin.User };
        _handler.Setup(dh => dh.GetUsersAsync()).ReturnsAsync(users);

        var result = await _userManager.GetUsersAsync();
        Assert.AreEqual(3, result.Count);
        CollectionAssert.AreEqual(users, result);
    }

    [TestMethod]
    public async Task GetStudentByUser_ShouldThrowException_WhenStudentNotFound()
    {
        _handler.Setup(dh => dh.GetStudentByUserAsync(_testUser)).ReturnsAsync((Student?)null);
        await Assert.ThrowsExceptionAsync<Exception>(() => _userManager.GetStudentByUserAsync(_testUser));
    }

    [TestMethod]
    public async Task GetStudentByUser_ShouldReturnStudent_WhenStudentExists()
    {
        _handler.Setup(dh => dh.GetStudentByUserAsync(_testUser)).ReturnsAsync(_testStudent);

        var result = await _userManager.GetStudentByUserAsync(_testUser);
        Assert.AreEqual(_testStudent, result);
    }

}

