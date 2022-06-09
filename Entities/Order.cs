using AppCore.Records;

namespace Entities;

public class Order : BaseRecordCore
{
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public EnumOrderStatus Status { get; set; }
    public List<BookOrder> BookOrders { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int Note { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
}

public enum EnumOrderStatus
{
    Received = 0,
    Confirmed = 1,
    Canceled = -1,
    Completed = 2
}