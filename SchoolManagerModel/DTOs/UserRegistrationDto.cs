using SchoolManagerModel.Utils;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagerModel.DTOs;
public class UserRegistrationDto
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = "";

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = "";

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = "";

    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = "";

    [Required]
    [Display(Name = "Role")]
    public string Role { get; set; } = "";

    public int? AssignedClassId { get; set; }
    public List<int>? AssignedSubjects { get; set; }

    public string Name => CultureUtils.GetFullName(FirstName, LastName);
}
