using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Entities
{
    public class Mark
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Grade { get; set; }
        required public Student Student { get; set; }
        required public Subject Subject { get; set; }

        required public DateTime SubmitDate { get; set; }
        public string Notes { get; set; } = string.Empty;

    }
}
