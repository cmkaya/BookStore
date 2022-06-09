using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class AuthorDto : BaseRecordCore
{
    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength(100, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength(100, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [DisplayName("Last Name")]
    public string LastName { get; set; }
    public DateTime? Birthdate { get; set; }
    
    [StringLength(1000, ErrorMessage = "The {0} must be maximum {1} characters!")]
    public string Bio { get; set; }
    [DisplayName("Bio")]
    public string ShortBio { get; set; }

    [DisplayName("Birthdate")]
    public string BirthdateText { get; set; }

    [DisplayName("Author")]
    public string FullNameDisplay { get; set; }
}