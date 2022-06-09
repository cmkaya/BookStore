using AppCore.Business.Configs;
using Business.Abstracts;
using Business.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebUI.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;

    public OrdersController(IOrderService orderService, ICountryService countryService, ICityService cityService)
    {
        _orderService = orderService;
        _countryService = countryService;
        _cityService = cityService;
    }

    // public IActionResult Receive()
    // {
        // string cartJson = HttpContext.Session.GetString("cart");
        // if (string.IsNullOrWhiteSpace(cartJson))
        //     return RedirectToAction("Index", "Books");
        //
        // //Creating cart lists based on user.
        // List<CartItemDto> cartItemListDto = JsonConvert.DeserializeObject<List<CartItemDto>>(cartJson);
        //
        // //Removing user info in the session.
        // HttpContext.Session.Remove("cart");
        //
        // var orderDto = new OrderDto
        // {
        //     UserId = cartItemListDto.FirstOrDefault().UserId,
        //     OrderDate = DateTime.Now,
        //     BookOrderListDto = cartItemListDto.Select(c => new BookOrderDto {BookId = c.BookId}).ToList()
        // };
        //
        // var orderResult = _orderService.Add(orderDto);
        // if (orderResult.ResultStatus == ResultStatusConfig.Exception)
        //     throw new Exception(orderResult.Message);
        //
        // TempData["success"] = orderResult.Message;
        // return RedirectToAction(nameof(Index));
    // }

    public IActionResult CheckOut()
    {
        var cartItemsJson = GetFromSession();
        
        var cartsDto = (from item in cartItemsJson
            group item by new {item.BookId, item.BookName, item.UserId}
            into itemGroup
            orderby itemGroup.Key.BookName
            select new CartDto
            {
                BookId = itemGroup.Key.BookId,
                UserId = itemGroup.Key.UserId,
                BookName = itemGroup.Key.BookName,
                Quantity = itemGroup.Count(),
                UnitPrice = itemGroup.Sum(i => i.UnitPrice),
                TotalPrice = itemGroup.Sum(i => i.UnitPrice * i.Quantity)
            }).ToList();
        
        
        var orderDto = new OrderDto
        {
            UserId = cartItemsJson.FirstOrDefault().UserId,
            BookOrderListDto = cartItemsJson.Select(c => new BookOrderDto {BookId = c.BookId}).ToList(),
            Carts = cartsDto
        };
      

        var dataResultCountries = _countryService.GetAllCountries();
        if (dataResultCountries.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultCountries.Message);

        ViewBag.Countries = new SelectList(dataResultCountries.Data, "Id", "Name");
        return View(orderDto);
    }

    // [HttpPost]
    // public IActionResult CheckOut(OrderDto dto)
    // {
    //     return View("success");
    // }
    //
    // public JsonResult Index()
    // {
    //     var json = GetFromSession();
    //     return Json(json);
    // }
    private List<CartItemDto> GetFromSession()
    {
        var cartDtoList = new List<CartItemDto>();
        string cartJson = HttpContext.Session.GetString("cart");
        if (!string.IsNullOrWhiteSpace(cartJson))
            cartDtoList = JsonConvert.DeserializeObject<List<CartItemDto>>(cartJson);
        
        return cartDtoList;
    }
}