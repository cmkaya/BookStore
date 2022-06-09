using AppCore.Business.Configs;
using Business.Abstracts;
using Business.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class AuthorsController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }
    
    // GET
    public IActionResult Index()
    {
        return View(_authorService.Query().ToList());
    }
    
    //
    public IActionResult Details(int? id)
    {
        if (id is null)
            return View("NotFound");

        var authorToDisplay = _authorService.Query().SingleOrDefault(a => a.Id == id);
        if (authorToDisplay is null)
            return View("NotFound");

        return View(authorToDisplay);
    }
    
    // GET
    public IActionResult Edit(int? id)
    {
        if (id is null)
            return View("NotFound");

        var model = _authorService.Query().SingleOrDefault(a => a.Id == id);
        return model is null ? View("NotFound") : View(model);
    }
    
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorDto editedAuthorModel)
    {
        if (ModelState.IsValid)
        {
            var result = _authorService.Update(editedAuthorModel);
            switch (result.ResultStatus)
            {
                case ResultStatusConfig.Exception:
                    return View("Error");
                case ResultStatusConfig.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    ModelState.AddModelError("", result.Message);
                    break;
            }
        }

        return View(editedAuthorModel);
    }

    // GET
    public IActionResult Create()
    {
        return View(new AuthorDto());
    }
    
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AuthorDto modelToCreate)
    {
        if (ModelState.IsValid)
        {
            var result = _authorService.Add(modelToCreate);
            switch (result.ResultStatus)
            {
                case ResultStatusConfig.Exception:
                    return View("Error");
                case ResultStatusConfig.Success:
                    return RedirectToAction(nameof(Index));
                default:
                    ModelState.AddModelError("", result.Message);
                    break;
            }
        }

        return View(modelToCreate);
    }

    // POST
    // [HttpPost] TODO: It must be asked how the `Delete` action post without specify [HttpPost] 
    public IActionResult Delete(int? id)
    {
        if (id is null)
            return View("NotFound");

        var result = _authorService.Delete(id.Value);
        switch (result.ResultStatus)
        {
            case ResultStatusConfig.Error:
                return View("Error"); //TODO: Handle the error result view. What should it be?
            case ResultStatusConfig.Success:
                return RedirectToAction(nameof(Index));
        }

        return View("Error");
    }
}