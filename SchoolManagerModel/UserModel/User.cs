using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerModel.UserModel;

public class User
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public User()
    {

    }

    public User(string username, string password, string email, string firstName, string lastName)
    {
        Username = username;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;

    }


}
