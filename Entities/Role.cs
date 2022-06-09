using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class Role : BaseRecordCore
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public List<User> Users { get; set; }
}