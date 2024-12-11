using SchoolManagerModel.Entities;

namespace SchoolManagerModel.Utils;
public static class StringRoleConverter
{
    public static Role GetRole(string roleName)
    {
        roleName = roleName.ToLower();
        return roleName switch
        {
            "admin" => Role.Administrator,
            "teacher" => Role.Teacher,
            "student" => Role.Student,
            _ => throw new Exception()
        };
    }

    public static string GetRoleString(Role role)
    {
        return role switch
        {
            Role.Administrator => "Admin",
            Role.Teacher => "Teacher",
            Role.Student => "Student",
            _ => throw new Exception()
        };
    }
}
