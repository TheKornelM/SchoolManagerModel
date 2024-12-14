using SchoolManagerModel.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolManagerModel.DTOs;
public class UserRegistrationDto
{
    private string _role = string.Empty;
    private int? _assignedClassId;

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
    public string Role
    {
        get => _role;
        set
        {
            if (_role == value)
            {
                return;
            }

            _role = value;
            RoleModified.Invoke(this, EventArgs.Empty);
        }
    }
    //public string Role { get; set; } = string.Empty;

    public int? AssignedClassId
    {
        get => _assignedClassId;
        set
        {
            _assignedClassId = value; 
            ClassModified.Invoke(this, EventArgs.Empty);
        }
    }

    public List<int>? AssignedSubjects { get; set; }

    public string Name => CultureUtils.GetFullName(FirstName, LastName);
    
    [JsonIgnore]
    public EventHandler RoleModified { get; set; } = delegate {  };
    [JsonIgnore]
    public EventHandler ClassModified { get; set; } = delegate { };
}
