using AppCore.Business.Paging;
using Business.DataTransferObjects;
using Business.DataTransferObjects.FilterAndPaging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Models;

public class BookViewIndexModel
{
    // public int? AuthorId { get; set; }
    public List<BookDto> Books { get; set; }
    public FilteredBookDto FilterBy { get; set; }
    public PageInfo PageInfo { get; set; }
    public SelectList SelectPagesList { get; set; }
    public int CurrentPage { get; set; }
}