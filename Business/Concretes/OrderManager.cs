using System.Globalization;
using AppCore.Business.Results;
using Business.Abstracts;
using Business.Constants;
using Business.DataTransferObjects;
using DataAccess.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes;

public class OrderManager : IOrderService
{
    private readonly BaseOrderDal _orderDal;
    private readonly BaseBookDal _bookDal;
    private readonly BaseBookOrderDal _bookOrderDal;
    private readonly BaseAuthorDal _authorDal;
    private readonly BaseUserDal _userDal;
    private readonly BaseUserDetailDal _userDetailDal;
    private readonly BaseCountryDal _countryDal;
    private readonly BaseCityDal _cityDal;

    public OrderManager(BaseOrderDal orderDal, BaseBookDal bookDal, BaseBookOrderDal bookOrderDal, BaseAuthorDal authorDal, BaseUserDal userDal, BaseUserDetailDal userDetailDal, BaseCountryDal countryDal, BaseCityDal cityDal)
    {
        _orderDal = orderDal;
        _bookDal = bookDal;
        _bookOrderDal = bookOrderDal;
        _authorDal = authorDal;
        _userDal = userDal;
        _userDetailDal = userDetailDal;
        _countryDal = countryDal;
        _cityDal = cityDal;
    }
    
    public IQueryable<OrderDto> Query()
    { 
        // var orderQuery = _orderDal.Query();
        // var userQuery = _userDal.Query();
        // var userDetailQuery = _userDetailDal.Query();
        // var countryQuery = _countryDal.Query();
        // var cityQuery = _cityDal.Query();
        // var bookOrderQuery = _bookOrderDal.Query();
        // var bookQuery = _bookDal.Query();
        // var authorQuery = _authorDal.Query();
        // var query = from o in orderQuery
        //     join u in userQuery on o.UserId equals u.Id
        //     join ud in userDetailQuery on u.UserDetailId equals ud.Id
        //     join co in countryQuery on ud.CountryId equals co.Id
        //     join ci in cityQuery on ud.CityId equals ci.Id
        //     join bo in bookOrderQuery on o.Id equals bo.OrderId
        //     join b in bookQuery on bo.BookId equals b.Id
        //     join a in authorQuery on b.AuthorId equals a.Id
        //     select new OrderDto
        //     {
        //         Id = o.Id,
        //         UserId = o.UserId,
        //         OrderDate = o.OrderDate,
        //         OrderDateText = o.OrderDate.ToString(new CultureInfo("en-US")),
        //         OrderStatus = o.Status,
        //         UserDto = new UserDto
        //         {
        //             UserName = u.UserName,
        //             UserDetail = new UserDetailDto
        //             {
        //                 EMail = ud.EMail,
        //                 Address = ud.Address,
        //                 Country = new CountryDto {Id = co.Id, Name = co.Name},
        //                 City = new CityDto {Id = ci.Id, Name = ci.Name}
        //             }
        //         },
        //         BookOrderDtoJoin = new BookOrderDto
        //         {
        //             OrderId = bo.OrderId,
        //             BookId = bo.BookId,
        //             BookDto = new BookDto
        //             {
        //                 Name = b.Name,
        //                 Publisher = b.Publisher,
        //                 StockAmount = b.StockAmount,
        //                 UnitPrice = b.UnitPrice,
        //                 UnitPriceText = b.UnitPrice.ToString(new CultureInfo("en-US")),
        //                 PublicationDate = b.PublicationDate,
        //                 PublicationDateText = b.PublicationDate.HasValue
        //                     ? b.PublicationDate.Value.ToString("MM/dd/yyyy",
        //                         new CultureInfo("en-US"))
        //                     : "",
        //                 Author = new AuthorDto {FullNameDisplay = a.FirstName + " " + a.LastName}
        //             }
        //         }
        //     };
        //
        // query = query.OrderBy(q => q.OrderDate);
            //.OrderByDescending(q => q.OrderDate)
            //.ThenBy(q => q.OrderStatus)
            //.ThenBy(q => q.BookOrderDtoJoin.BookDto.AuthorNameDisplay)
            //.ThenBy(q => q.BookOrderDtoJoin.BookDto.Name);

        var orderQuery = _orderDal.Query();
        var userQuery = _userDal.Query();
        var userDetailQuery = _userDetailDal.Query();
        var countryQuery = _countryDal.Query();
        var cityQuery = _cityDal.Query();
        var bookOrderQuery = _bookOrderDal.Query();
        var bookQuery = _bookDal.Query();
        var authorQuery = _authorDal.Query();
        var query = from o in orderQuery
            join u in userQuery on o.UserId equals u.Id
            join ud in userDetailQuery on u.UserDetailId equals ud.Id
            join co in countryQuery on ud.CountryId equals co.Id
            join ci in cityQuery on ud.CityId equals ci.Id
            join bo in bookOrderQuery on o.Id equals bo.OrderId
            join b in bookQuery on bo.BookId equals b.Id
            join a in authorQuery on b.AuthorId equals a.Id
            select new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                OrderDateText = o.OrderDate.ToShortDateString(),
                OrderStatus = o.Status,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Phone = o.Phone,
                Address = o.Address,
                CityId = o.CityId,
                CityDisplay = o.City.Name,
                CountryId = o.Country.Id,
                CountryDisplay = o.Country.Name,
                BookOrderDtoJoin = new BookOrderDto
                {
                    OrderId = bo.OrderId,
                    BookId = bo.BookId,
                    Price = bo.Price,
                    Quantity = bo.Quantity,
                    BookDto = new BookDto
                    {
                        Name = b.Name,
                        Publisher = b.Publisher,
                        PublicationDate = b.PublicationDate,
                        PublicationDateText = b.PublicationDate.HasValue
                            ? b.PublicationDate.Value.ToShortDateString()
                            : "",
                        AuthorNameDisplay = b.Author.FirstName + " " + b.Author.LastName
                    }
                }
            };

        query = query.OrderBy(q => q.OrderDate);
            
        return query;
    }

    public Result Add(OrderDto dto)
    {
        try
        {
            var orderToCreate = new Order
            {
                UserId = dto.UserId,
                Status = dto.OrderStatus,
                OrderDate = dto.OrderDate,
                BookOrders = dto.BookOrderListDto.Select(bo => new BookOrder {BookId = bo.BookId}).ToList()
            };
            
            _orderDal.Add(orderToCreate);
            return new SuccessResult(Messages.OrderAdded);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public Result Update(OrderDto dto)
    {
        throw new NotImplementedException();
    }

    public Result Delete(int id)
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        _orderDal.Dispose();
    }
}