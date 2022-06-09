using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class GenreDto : BaseRecordCore
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}