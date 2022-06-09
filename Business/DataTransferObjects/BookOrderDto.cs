using AppCore.Records;

namespace Business.DataTransferObjects;

public class BookOrderDto : BaseRecordCore
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public OrderDto OrderDto { get; set; }
    public BookDto BookDto { get; set; }

   
    
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}