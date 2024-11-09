using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagerModel.UserModel;

namespace SchoolManagerModel
{
    public class AssignedSubject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        required public Subject Subject { get; set; }
        required public Student Student { get; set; }

        public List<Mark>? Marks { get; set; }

        public bool GotGrade { get; set; }
        public int? Mark { get; set; }
    }
}
