using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class CountryDto : BaseRecordCore
{
    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength(75, ErrorMessage = "The {0} must be maximum {1} characters!")]
    [DisplayName("Country")]
    public string Name { get; set; }
}