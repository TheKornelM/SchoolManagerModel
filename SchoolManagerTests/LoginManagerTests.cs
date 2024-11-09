using Moq;
using SchoolManagerModel;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.UserModel;

namespace SchoolManagerTests;

[TestClass]
public partial class LoginManagerTests
{
    readonly Mock<IAsyncUserDataHandler> _handler;
    readonly LoginManager _manager;

    public LoginManagerTests()
    {
        _handler = new Mock<IAsyncUserDataHandler>();
        _manager = new LoginManager(_handler.Object);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "User not found")]
    public async Task UserNotFoundTest()
    {
        _handler.Setup(m => m.GetUserAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<User?>(null));
        await _manager.LoginAsync("test", "test");
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Invalid password")]
    public async Task InvalidPasswordTest()
    {
        var user = new User("test", "test", "test", "firstName", "lastName");

        _handler.Setup(m => m.GetUserAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<User?>(user));
        await _manager.LoginAsync(user.Username, "incorrectPw");
    }

    [TestMethod]
    public async Task CorrectCredentials()
    {
        var user = new User("test", "test", "test", "firstName", "lastName");
        var userHashedPassword = new User("test", HashStringMD5.GetHashedString("test"), "test", "firstName", "lastName");
        _handler.Setup(m => m.GetUserAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<User?>(userHashedPassword));
        await _manager.LoginAsync(user.Username, user.Password);
    }
}