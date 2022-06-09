using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class User : BaseRecordCore
{
    [Required]
    [MinLength(6), MaxLength(20)]
    public string UserName { get; set; }
    [Required]
    [MinLength(4), MaxLength(12)]
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int UserDetailId { get; set; }
    public UserDetail UserDetail { get; set; }
    public List<Order> Orders { get; set; }
}