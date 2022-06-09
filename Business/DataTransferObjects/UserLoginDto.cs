using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.DataTransferObjects;

public class UserLoginDto
{
    [Required(ErrorMessage = "The {0} is required!")]
    [MinLength(6, ErrorMessage = "The {0} must be minimum {1} characters!")]
    [MaxLength(20, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [DisplayName("User Name")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [MinLength(4, ErrorMessage = "The {0} must be minimum {1} characters!")]
    [MaxLength(12, ErrorMessage = "The {0} must be maximum {1} characters!")]
    public string Password { get; set; }
}