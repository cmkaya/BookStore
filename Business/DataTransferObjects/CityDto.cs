using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class CityDto : BaseRecordCore
{
    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength(75, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [DisplayName("City")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [DisplayName("Country")]
    public int CountryId { get; set; }
    
    public CountryDto Country { get; set; }
}