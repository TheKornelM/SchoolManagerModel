using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolManagerModel.DTOs;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Persistence;

namespace SchoolManagerModel.Managers
{
    public class UserManager : UserManager<User>
    {
        private readonly IAsyncUserDataHandler _dataHandler;

        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
            IAsyncUserDataHandler dataHandler)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
            _dataHandler = dataHandler;
        }

        public async Task<bool> IsAdminAsync(User user)
        {
            return await GetRoleAsync(user) == Role.Administrator;
        }

        public async Task<Role> GetRoleAsync(User user)
        {
            Role? userRole = await _dataHandler.GetRoleAsync(user);
            return userRole ?? Role.Student; // Return the role or default to Student
        }

        public async Task<bool> UserExistsAsync(User user)
        {
            return await _dataHandler.UsernameExistsAsync(user.UserName);
        }

        public async Task<bool> EmailAlreadyRegisteredAsync(string email)
        {
            return await _dataHandler.EmailAlreadyRegisteredAsync(email);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _dataHandler.AddStudentAsync(student);
        }

        public async Task AddTeacherAsync(Teacher teacher)
        {
            await _dataHandler.AddTeacherAsync(teacher);
        }

        public async Task AddAdminAsync(Admin admin)
        {
            await _dataHandler.AddAdminAsync(admin);
        }

        public async Task AssignRoleAsync(User user, Role role)
        {
            if (!await UserExistsAsync(user))
            {
                throw new Exception("User not registered");
            }

            await _dataHandler.AssignRoleAsync(user, role);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            if (!await UserExistsAsync(new User { UserName = username }))
            {
                throw new Exception("User not found");
            }

            return await _dataHandler.GetUserByUsernameAsync(username);
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            return await _dataHandler.GetUsersAsync();
        }

        public async Task<Student> GetStudentByUserAsync(User user)
        {
            var result = await _dataHandler.GetStudentByUserAsync(user);
            return result ?? throw new Exception("Student not found");
        }

        public async Task<Teacher?> GetTeacherByUserAsync(User user)
        {
            return await _dataHandler.GetTeacherByUserAsync(user);
        }

        public async Task<List<UserDto>> FilterUsersAsync(string? username = null, string? firstName = null,
            string? lastName = null, string? email = null)
        {
            return await _dataHandler.FilterUsersAsync(username, firstName, lastName, email);
        }
    }
}