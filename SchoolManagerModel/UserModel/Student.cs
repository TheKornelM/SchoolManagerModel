using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerModel.UserModel;

public class Student
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    required public User User { get; set; }

    required public Class Class { get; set; }

    public List<AssignedSubject>? Subjects { get; set; }

}
