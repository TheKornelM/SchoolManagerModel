using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Entities
{
    public class Subject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        required public string Name { get; set; }
        required public Class Class { get; set; }
        required public Teacher Teacher { get; set; }

        // Required for Entity Framework
        public Subject()
        {

        }

        public Subject(string name, Class @class, Teacher teacher)
        {
            Name = name;
            Class = @class;
            Teacher = teacher;
        }


    }
}
