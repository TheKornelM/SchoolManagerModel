using Moq;
using SchoolManagerModel;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
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
    public async Task UserExistsAsync_ShouldReturnTrue_WhenUserExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.Username)).ReturnsAsync(true);

        var result = await _userManager.UserExistsAsync(_testUser);
        Assert.IsTrue(result);
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


}

