using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagerModel.UserModel;

namespace SchoolManagerModel
{
    public class Lesson
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        required public Teacher TeacherId { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
    }
}
