using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;
using Entities;

namespace Business.DataTransferObjects;

public class UserRegisterDto : BaseRecordCore
{
    [Required(ErrorMessage = "The {0} is required!")]
    [MinLength(6, ErrorMessage = "The {0} must be minimum {1} characters!")]
    [MaxLength(20, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [DisplayName("User Name")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [MinLength(4, ErrorMessage = "The {0} must be minimum {1} characters!")]
    [MaxLength(12, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [PasswordPropertyText]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [MinLength(4, ErrorMessage = "The {0} must be minimum {1} characters!")]
    [MaxLength(12, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [PasswordPropertyText]
    [Compare("Password", ErrorMessage = "The {0} and {1} must be the same!")]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; }
    
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public int UserDetailId { get; set; }
    public UserDetailDto UserDetail { get; set; }
}