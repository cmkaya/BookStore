using AppCore.Business.Configs;
using AppCore.Business.Results;
using Business.Abstracts;
using Business.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents;

public class AuthorsViewComponent : ViewComponent
{
    private readonly IAuthorService _authorService;

    public AuthorsViewComponent(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public IViewComponentResult Invoke(int? id)
    {
        Task<DataResult<List<AuthorDto>>> task = _authorService.GetAuthorsAsync();
        var dataResultAuthorsDto = task.Result;
        if (dataResultAuthorsDto.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultAuthorsDto.Message);

        var authorsDto = dataResultAuthorsDto.Data;
        ViewBag.AuthorId = id;
        return View(authorsDto);
    }
}