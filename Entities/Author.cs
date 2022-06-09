using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class Author : BaseRecordCore
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string LastName { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? Birthdate { get; set; }
    
    [StringLength(1000)]
    public string Bio { get; set; }
    public List<Book> Books { get; set; }
    [StringLength(5)]
    public string FileExtension { get; set; }
}