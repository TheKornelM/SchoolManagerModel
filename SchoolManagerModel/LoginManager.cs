using SchoolManagerModel.Persistence;

namespace SchoolManagerModel;

public class LoginManager(IAsyncUserDataHandler schoolData)
{
    readonly IAsyncUserDataHandler _schoolData = schoolData;
    public async Task LoginAsync(string username, string password)
    {
        var result = await _schoolData.GetUserAsync(username)
            ?? throw new Exception("User not found");


        string hashedPassword = HashStringMD5.GetHashedString(password);

        if (hashedPassword != result.Password)
        {
            throw new Exception("Invalid password!");
        }
    }


}