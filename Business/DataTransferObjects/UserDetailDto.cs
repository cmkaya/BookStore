using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class UserDetailDto : BaseRecordCore
{
    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength (100, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [EmailAddress(ErrorMessage = "The {0} is not valid!")]
    public string EMail { get; set; }

    [Required(ErrorMessage = "The {0} is required!")]
    [DisplayName("Country")]
    public int CountryId { get; set; }
    public CountryDto Country { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [DisplayName("City")]
    public int CityId { get; set; }
    public CityDto City { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength(1000, ErrorMessage = "The {0} must be maximum {1} characters!")]
    public string Address { get; set; }
}