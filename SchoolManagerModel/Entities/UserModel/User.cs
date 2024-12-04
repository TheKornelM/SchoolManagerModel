using Microsoft.AspNetCore.Identity;
using SchoolManagerModel.Utils;

namespace SchoolManagerModel.Entities.UserModel;

public class User : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Name => CultureUtils.GetFullName(FirstName, LastName);

    public User()
    {

    }

    public User(string username, string password, string email, string firstName, string lastName)
    {
        UserName = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = password;

    }


}
