using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class City : BaseRecordCore
{
    [Required]
    [StringLength(75)]
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public List<UserDetail> UserDetails { get; set; }
}