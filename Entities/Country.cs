using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class Country : BaseRecordCore
{
    [Required]
    [StringLength(75)]
    public string Name { get; set; }
    public List<City> Cities { get; set; }
    public List<UserDetail> UserDetails { get; set; }
}