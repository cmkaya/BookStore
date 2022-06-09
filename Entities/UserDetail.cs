using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class UserDetail : BaseRecordCore
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string EMail { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    [Required]
    [StringLength(1000)]
    public string Address { get; set; }
    public User User { get; set; }
}