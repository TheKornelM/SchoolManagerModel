using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerModel.Entities;

public class Class
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required int Year { get; set; }
    public required string SchoolClass { get; set; }

    public string Name => $"{Year}/{SchoolClass}";

    public Class()
    {
    }

    public Class(int year, string schoolClass)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(year, 1);

        var classChar = schoolClass.ToUpper()[0];

        if (!Char.IsLetter(classChar))
        {
            throw new FormatException("School class must be a letter!");
        }

        Year = year;
        SchoolClass = classChar.ToString();
    }
}