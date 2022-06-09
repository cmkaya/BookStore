using System.ComponentModel.DataAnnotations;
using AppCore.Records;

namespace Entities;

public class Book : BaseRecordCore
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [StringLength(150)]
    public string Publisher { get; set; }
    public int Page { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? PublicationDate { get; set; }
    
    [DataType(DataType.Currency)]
    public decimal UnitPrice { get; set; }
    public short StockAmount { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    //public List<BookGenre> BookGenres { get; set; }
    public List<BookOrder> BookOrders { get; set; }
    [StringLength(5)]
    public string FileExtension { get; set; }
}