using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Business.DataAnnotations;

namespace Business.DataTransferObjects.FilterAndPaging;

public class FilteredBookDto
{
    public int? AuthorId { get; set; }
    
    [DisplayName("Book Name")]
    [StringLength(200)]
    public string BookName { get; set; }

    [DisplayName("Unit Price")]
    [StringToDecimal(ErrorMessage = "Unit price must be numeric!")]
    public string BeginningPriceText { get; set; }
    
    [StringToDecimal(ErrorMessage = "Unit price must be numeric!")]
    public string EndPriceText { get; set; }
    
    [DisplayName("Author")]
    public string AuthorFullName { get; set; }
    
    [DisplayName("Stock Amount")]
    public short? BeginningStockAmount { get; set; }
    public short? EndStockAmount { get; set; }
    
    [DisplayName("Publication Date")]
    public string BeginningPublicationDateText { get; set; }
    public string EndPublicationDateText { get; set; }
    
}