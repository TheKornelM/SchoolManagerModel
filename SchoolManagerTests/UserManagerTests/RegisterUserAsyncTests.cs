using Moq;
using SchoolManagerModel.UserModel;

namespace SchoolManagerTests.UserManagerTests;

public partial class UserManagerTests
{
    [TestMethod]
    [ExpectedException(typeof(Exception), "User already registered")]
    public async Task UserAlreadyRegisteredTest()
    {
        var user = new User("test", "test", "test", "firstName", "lastName");
        _handler.Setup(m => m.UsernameExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        await _userManager.RegisterUserAsync(user);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Email already registered")]
    public async Task EmailAlreadyRegisteredTest()
    {
        var user = new User("test", "test", "test", "firstName", "lastName");
        _handler.Setup(m => m.UsernameExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        _handler.Setup(m => m.EmailAlreadyRegisteredAsync(It.IsAny<string>())).ReturnsAsync(true);
        await _userManager.RegisterUserAsync(user);
    }

    [TestMethod]
    public async Task SuccessfulRegistration()
    {
        List<User> users = [];
        var user = new User("test", "test", "test", "firstName", "lastName");

        _handler.Setup(m => m.AddUserAsync(It.IsAny<User>())).Callback(() => users.Add(user));
        await _userManager.RegisterUserAsync(user);

        _handler.Setup(m => m.UsernameExistsAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(users.Contains(user)));
        Assert.IsTrue(await _userManager.UserExistsAsync(user));
    }

}
