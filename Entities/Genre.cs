using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class Genre : BaseRecordCore
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    public List<BookGenre> BookGenres { get; set; }
}