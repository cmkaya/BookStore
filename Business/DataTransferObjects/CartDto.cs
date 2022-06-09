using System.ComponentModel;

namespace Business.DataTransferObjects;

public class CartDto
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    
    [DisplayName("Book Name")]
    public string BookName { get; set; }
    [DisplayName("Unit Price")]
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string ImageUrl { get; set; }
    
    public List<CartItemDto> CartItems { get; set; }

    public decimal GetTotalPrice()
    {
        return CartItems.Sum(i => i.UnitPrice * i.Quantity);
    }
}