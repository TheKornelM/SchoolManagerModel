using SchoolManagerModel.Utils;

namespace SchoolManagerModel.Entities.UserModel;

public class User : ApplicationUser
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
