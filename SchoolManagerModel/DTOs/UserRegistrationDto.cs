using SchoolManagerModel.Utils;

namespace SchoolManagerModel.DTOs;
public class UserRegistrationDto
{
    public string Username { get; set; }
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Name => CultureUtils.GetFullName(FirstName, LastName);

    public string Role { get; set; }

    public string SelectedClassId { get; set; }

    public string Password { get; set; }

    public UserRegistrationDto()
    {

    }

    public UserRegistrationDto(string username, string email, string firstName, string lastName, string role, string password)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Password = password;
    }
}
