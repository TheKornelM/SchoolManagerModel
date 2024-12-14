﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SchoolManagerModel.DTOs;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;

namespace SchoolManagerTests.UserManagerTests;

[TestClass]
public partial class UserManagerTests
{
    private readonly Mock<IAsyncUserDataHandler> _handler;
    private readonly Mock<IUserStore<User>> _userStore;
    private readonly UserManager _userManager;

    private readonly User _testUser;
    private readonly Student _testStudent;
    private readonly Teacher _testTeacher;
    private readonly Admin _testAdmin;

    public UserManagerTests()
    {
        _handler = new Mock<IAsyncUserDataHandler>();
        _userStore = new Mock<IUserStore<User>>();
        var options = Options.Create(new IdentityOptions());
        var passwordHasher = new Mock<IPasswordHasher<User>>();
        var userValidators = new List<IUserValidator<User>> { new Mock<IUserValidator<User>>().Object };
        var passwordValidators = new List<IPasswordValidator<User>> { new Mock<IPasswordValidator<User>>().Object };
        var keyNormalizer = new Mock<ILookupNormalizer>();
        var errors = new IdentityErrorDescriber();
        var services = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<User>>>();

        // Initialize UserManager with mocks
        _userManager = new UserManager(
            _userStore.Object,
            options,
            passwordHasher.Object,
            userValidators,
            passwordValidators,
            keyNormalizer.Object,
            errors,
            services.Object,
            logger.Object,
            _handler.Object
        );
        _testUser = new User("username", "password", "email@test.com", "firstName", "lastName");
        _testStudent = new Student { Id = 1, User = _testUser, Class = new Class() };
        _testTeacher = new Teacher { Id = 2, User = new User("teacher", "password", "teacher@test.com", "firstName", "lastName") };
        _testAdmin = new Admin { Id = 3, User = new User("admin", "password", "admin@test.com", "firstName", "lastName") };
    }

    [TestMethod]
    public async Task UserExistsAsync_ShouldReturnTrue_WhenUserExists()
    {
        _handler.Setup(dh => dh.UsernameExistsAsync(_testUser.UserName)).ReturnsAsync(true);

        var result = await _userManager.UserExistsAsync(_testUser);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task GetUsersAsync_ShouldReturnAllUsers()
    {
        var users = new List<User> { _testUser, _testTeacher.User, _testAdmin.User }.Select(user => new UserDto()
        {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }).ToList();
        _handler.Setup(dh => dh.GetUsersAsync()).ReturnsAsync(users);

        var result = await _userManager.GetUsersAsync();
        Assert.AreEqual(3, result.Count);
        CollectionAssert.AreEqual(users, result);
    }


}

