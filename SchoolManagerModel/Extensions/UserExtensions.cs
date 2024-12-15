using SchoolManagerModel.DTOs;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Extensions;

public static class UserExtensions
{
    public static User ToUser(this UserRegistrationDto userDto)
    {
        return new User
        {
            UserName = userDto.Username,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };
    }

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto()
        {
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName ?? string.Empty,
        };
    }
}