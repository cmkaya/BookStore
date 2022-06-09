using AppCore.Records;

namespace Entities;

public class BookOrder : BaseRecordCore
{
    public int BookId { get; set; }
    public int OrderId { get; set; }
    public Book Book { get; set; }
    public Order Order { get; set; }


    public decimal Price { get; set; }
    public int Quantity { get; set; }
}