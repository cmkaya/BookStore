using System.Security.Claims;
using Business.Abstracts;
using Business.Constants;
using Business.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebUI.Controllers;

public class CartController : Controller
{
    private readonly IBookService _bookService;

    public CartController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var cartItemsDto = GetFromSession();
        var cartsDto = (from item in cartItemsDto
            group item by new {item.BookId, item.BookName, item.UserId}
            into itemGroupBy
            orderby itemGroupBy.Key.BookName
            select new CartDto
            {
                BookId = itemGroupBy.Key.BookId,
                UserId = itemGroupBy.Key.UserId,
                BookName = itemGroupBy.Key.BookName,
                Quantity = itemGroupBy.Count(),
                UnitPrice = itemGroupBy.Sum(itemGroup => itemGroup.UnitPrice),
                TotalPrice = itemGroupBy.Sum(itemGroup => itemGroup.UnitPrice * itemGroup.Quantity)
            }).ToList();

        return View(cartsDto);
    }

    public IActionResult AddToCart(int? bookId)
    {
        if (!bookId.HasValue)
            return View("NotFound");

        var bookDto = _bookService.Query().SingleOrDefault(b => b.Id == bookId.Value);
        if (bookDto is null)
            return View("NotFound");

        if (bookDto.StockAmount == 0)
        {
            TempData["warning"] = Messages.NoStock;
            return RedirectToAction("Index", "Books");
        }

        var cartDtoList = GetFromSession();
        var cartDto = new CartItemDto
        {
            BookId = bookId.Value,
            UserId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value),
            BookName = bookDto.Name,
            UnitPrice = bookDto.UnitPrice
        };
        
        cartDtoList.Add(cartDto);
        int quantity = cartDtoList.Count(c => c.BookId == bookId);
        if (quantity > bookDto.StockAmount)
        {
            TempData["warning"] = Messages.InsufficientStock;
        }
        else
        {
            string cartJson = JsonConvert.SerializeObject(cartDtoList);
            HttpContext.Session.SetString("cart", cartJson);
            TempData["info"] = $"{cartDto.BookName} is added to the cart!";
        }
        
        return RedirectToAction("Details", "Books", new { id = bookId});
    }

    public IActionResult RemoveFromCart(int? bookId, int? userId)
    {
        if (!bookId.HasValue || !userId.HasValue)
            return View("NotFound");

        var cartDtoList = GetFromSession();
        if (cartDtoList is not null)
        {
            var cartDto = cartDtoList.FirstOrDefault(c => c.BookId == bookId.Value && c.UserId == userId.Value);
            if (cartDto is not null)
                cartDtoList.Remove(cartDto);
        }
        string cartJson = JsonConvert.SerializeObject(cartDtoList);
        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cartDtoList));

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove("cart");
        TempData["info"] = Messages.CartCleared;
        return RedirectToAction(nameof(Index));
    }
    
    private List<CartItemDto> GetFromSession()
    {
        var cartDtoList = new List<CartItemDto>();
        string cartJson = HttpContext.Session.GetString("cart");
        if (!string.IsNullOrWhiteSpace(cartJson))
            cartDtoList = JsonConvert.DeserializeObject<List<CartItemDto>>(cartJson);
        
        return cartDtoList;
    }
}