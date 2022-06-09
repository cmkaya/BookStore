using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class RoleDto : BaseRecordCore
{
    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength(50, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [DisplayName("Role")]
    public string Name { get; set; }
}