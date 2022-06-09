using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records;
using Entities;

namespace Business.DataTransferObjects;

public class OrderDto : BaseRecordCore
{
    public int UserId { get; set; }
    // public int OrderId { get; set; }
    
    public DateTime OrderDate { get; set; }

    [DisplayName("Order Date")]
    public string OrderDateText { get; set; }


    [DisplayName("Order Status")]
    public EnumOrderStatus OrderStatus { get; set; }
    public UserDto UserDto { get; set; }
    public List<BookOrderDto> BookOrderListDto { get; set; }
    public BookOrderDto BookOrderDtoJoin { get; set; }

    public List<CartDto> Carts { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string Phone { get; set; }
    public string Address { get; set; }
    public int Note { get; set; }
    public int CityId { get; set; }
    public string CityDisplay { get; set; }
    public int CountryId { get; set; }
    public string CountryDisplay { get; set; }
}