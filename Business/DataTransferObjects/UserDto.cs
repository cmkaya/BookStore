using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class UserDto : BaseRecordCore
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
    
    public bool IsActive { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [DisplayName("Role")]
    public int RoleId { get; set; }
    public RoleDto Role { get; set; }
    
    public int UserDetailId { get; set; }
    public UserDetailDto UserDetail { get; set; }
}