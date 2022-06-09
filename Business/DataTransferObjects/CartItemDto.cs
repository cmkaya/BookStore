using System.ComponentModel;

namespace Business.DataTransferObjects;

public class CartItemDto
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    
    [DisplayName("Book Name")]
    public string BookName { get; set; }
    
    [DisplayName("Unit Price")]
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}