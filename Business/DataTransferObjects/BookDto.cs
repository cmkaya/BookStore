using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Business.DataAnnotations;
using AppCore.Records;

namespace Business.DataTransferObjects;

public class BookDto : BaseRecordCore
{
    [Required(ErrorMessage = "The {0} is required!")]
    [StringLength(200, ErrorMessage = "The {0} must be maximum {1} characters!")]
    public string Name { get; set; }

    [StringLength(1000, ErrorMessage = "The {0} must be maximum {1} characters!")]
    public string Description { get; set; }
    public string Publisher { get; set; }
    public int Page { get; set; }
    public DateTime? PublicationDate { get; set; }
    public decimal UnitPrice { get; set; }
    public short StockAmount { get; set; }
    
    [Required(ErrorMessage = "The {0} is required!")]
    [DisplayName("Author")]
    public int AuthorId { get; set; }

    [DisplayName("Publication Date ")]
    public string PublicationDateText { get; set; }

    [Required(ErrorMessage = "The {0} is required!")]
    [StringToDecimal(ErrorMessage = "The {0} must be a number!")]
    [DisplayName("Unit Price")]
    public string UnitPriceText { get; set; }

    [DisplayName("Author")]
    public string AuthorNameDisplay { get; set; }

    public AuthorDto Author { get; set; }
    
    public string ShortDescription { get; set; }
    public string FileExtension { get; set; }
    [DisplayName("Image")]
    public string FilePath { get; set; }
}