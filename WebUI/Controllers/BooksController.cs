using AppCore.Business.Configs;
using AppCore.Business.Paging;
using Business.Abstracts;
using Business.DataTransferObjects;
using Business.DataTransferObjects.FilterAndPaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using WebUI.Models;
using WebUI.Settings;

namespace WebUI.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BooksController(IBookService bookService, IAuthorService authorService, IHttpContextAccessor httpContextAccessor)
    {
        _bookService = bookService;
        _authorService = authorService;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index(int? authorId)
    {
        
        var bookViewModel = new BookViewIndexModel
        {
            // AuthorId = authorId,
            FilterBy = new FilteredBookDto{AuthorId = authorId},
            PageInfo = new PageInfo{RecordPerPage = AppSettings.RecordPerPage}
        };
        
        var dataResultBooksDto = _bookService.GetBooksByFilter(bookViewModel.FilterBy, bookViewModel.PageInfo);
        if (dataResultBooksDto.ResultStatus == ResultStatusConfig.Exception)
            throw new Exception(dataResultBooksDto.Message);
        
        List<SelectListItem> pageSelectList = new List<SelectListItem>();
        if (bookViewModel.PageInfo.GetTotalPageNumber() == 0)
        {
            pageSelectList.Add(new SelectListItem
            {
                Value = "1",
                Text = "1"
            });
        }
        else
        {
            for (int i = 1; i <= bookViewModel.PageInfo.GetTotalPageNumber(); i++)
            {
                pageSelectList.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
        }
        
        bookViewModel.Books = dataResultBooksDto.Data;
        bookViewModel.SelectPagesList = new SelectList(pageSelectList, "Value", "Text");
            
        // var bookViewModel = new BookViewIndexModel
        // {
        //     AuthorId = authorId, 
        //     Books = dataResultBooksDto.Data, 
        //     FilterBy = filteredBookDto,
        // };
        return View(bookViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(BookViewIndexModel viewModel)
    {
        if (ModelState.IsValid)
        {
            PageInfo pageInfo = new PageInfo
            {
                RecordPerPage = AppSettings.RecordPerPage, 
                CurrentPage = viewModel.CurrentPage
            };
        
            var dataResultBooksDto = _bookService.GetBooksByFilter(viewModel.FilterBy, pageInfo);
            if (dataResultBooksDto.ResultStatus == ResultStatusConfig.Exception)
                throw new Exception(dataResultBooksDto.Message);
            
            List<SelectListItem> pageSelectList = new List<SelectListItem>();
            if (pageInfo.GetTotalPageNumber() == 0)
            {
                pageSelectList.Add(new SelectListItem
                {
                    Value = "1",
                    Text = "1"
                });
            }
            else
            {
                for (int i = 1; i <= pageInfo.GetTotalPageNumber(); i++)
                {
                    pageSelectList.Add(new SelectListItem()
                    {
                        Value = i.ToString(),
                        Text = i.ToString()
                    });
                }
            }
            
            viewModel.Books = dataResultBooksDto.Data;
            viewModel.SelectPagesList = new SelectList(pageSelectList, "Value", "Text", viewModel.CurrentPage);
        }

        // return View(viewModel);
        return PartialView("_BookCard", viewModel);
    }

    // GET: Books/Details/3
    public IActionResult Details(int? id)
    {
        if (id is null)
            return View("NotFound");

        var bookToDisplay = _bookService.Query().SingleOrDefault(b => b.Id == id);
        if (bookToDisplay is null)
            return View("NotFound");

        return View(bookToDisplay);
    }

    // GET: Books/Edit/4
    public IActionResult Edit(int? id)
    {
        if (id is null)
            return View("NotFound");

        BookDto modelToUpdate = _bookService.Query().SingleOrDefault(b => b.Id == id);
        if (modelToUpdate is null)
            return View("NotFound");

        ViewBag.Authors = new SelectList(_authorService.Query().ToList(), "Id", "FullNameDisplay");
        return View(modelToUpdate);
    }

    // POST: Books/Edit/4
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookDto editedDto, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            UpdateFileExtension(editedDto, file);
            var result = _bookService.Update(editedDto);
            if (result.ResultStatus == ResultStatusConfig.Exception)
                return View("Error");
            bool? isFileToSaved = IsImagedToSaved(editedDto, file);
            if (isFileToSaved == false)
            {
                TempData["error"] = $"The file cannot be uploaded because either the file extension is not {AppSettings.AcceptedImageExtensions} or file size more than {AppSettings.AcceptedImageMaxSize}!";
            }
            else
            {
                TempData["success"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", result.Message);
        }

        ViewBag.Authors = new SelectList(_authorService.Query().ToList(), "Id", "FullNameDisplay", editedDto.AuthorId);
        return View(editedDto);
    }

    // GET: Books/Create/5
    public IActionResult Create()
    {
        ViewBag.Authors = new SelectList(_authorService.Query().ToList(), "Id", "FullNameDisplay");
        return View(new BookDto());
    }

    // POST: Books/Create/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookDto createdDto, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            UpdateFileExtension(createdDto, file);
            var result = _bookService.Add(createdDto);
            if (result.ResultStatus == ResultStatusConfig.Exception)
                return View("Error");
            if (result.ResultStatus == ResultStatusConfig.Success)
            {
                bool? isFileToSaved = IsImagedToSaved(createdDto, file);
                if (isFileToSaved == false)
                {
                    TempData["error"] = $"The file cannot be uploaded because either the file extension is not {AppSettings.AcceptedImageExtensions} or file size more than {AppSettings.AcceptedImageMaxSize}!";
                }
                else
                    TempData["success"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", result.Message);
        }

        ViewBag.Authors = new SelectList(_authorService.Query().ToList(), "Id", "FullNameDisplay", createdDto.AuthorId);
        return View(createdDto);
    }

    // [HttpPost]
    public IActionResult Delete(int? id, string filePath)
    {
        if (id == null)
            return View("NotFound");

        BookDto imageToDelete = new BookDto {Id = id.Value, FilePath = filePath};
        var result = _bookService.Delete(id.Value);
        DeleteStaticFile(imageToDelete);
        if (result.ResultStatus == ResultStatusConfig.Error)
            return View("Error");
        
        if (result.ResultStatus == ResultStatusConfig.Success)
            return RedirectToAction(nameof(Index));

        return View("Error");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteFile(BookDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.FilePath))
        {
            var existingBookDto = _bookService.Query().SingleOrDefault(b => b.Id == dto.Id);
            existingBookDto.FileExtension = null;
            var result = _bookService.Update(existingBookDto);
            if (result.ResultStatus == ResultStatusConfig.Success)
                DeleteStaticFile(dto);
        }

        return RedirectToAction(nameof(Details), new {id = dto.Id});
    }


    public async Task Export()
    {
        string excelName = "Books";

        var dataResultProductsModel = _bookService.GetBooksByFilter();
        if (dataResultProductsModel is not null && dataResultProductsModel.Data.Count > 0)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(excelName);

            ////The 1st row: Heading
            excelWorksheet.Cells["A1"].Value = "Book Name";
            excelWorksheet.Cells["B1"].Value = "Author Name";
            excelWorksheet.Cells["C1"].Value = "Page";
            excelWorksheet.Cells["D1"].Value = "Publisher";
            excelWorksheet.Cells["E1"].Value = "Publication Date";
            excelWorksheet.Cells["F1"].Value = "Stock Amount";
            excelWorksheet.Cells["G1"].Value = "Unit Price";
            excelWorksheet.Cells["H1"].Value = "Description";
            
            ////The 2nd row: Data
            int itemQuantity = dataResultProductsModel.Data.Count;
            for (int row = 0; row < itemQuantity; row++)
            {
                excelWorksheet.Cells["A" + (row + 2)].Value = dataResultProductsModel.Data[row].Name;
                excelWorksheet.Cells["B" + (row + 2)].Value = dataResultProductsModel.Data[row].Author.FullNameDisplay;
                excelWorksheet.Cells["C" + (row + 2)].Value = dataResultProductsModel.Data[row].Page;
                excelWorksheet.Cells["D" + (row + 2)].Value = dataResultProductsModel.Data[row].Publisher;
                excelWorksheet.Cells["E" + (row + 2)].Value = dataResultProductsModel.Data[row].PublicationDateText;
                excelWorksheet.Cells["F" + (row + 2)].Value = dataResultProductsModel.Data[row].StockAmount;
                excelWorksheet.Cells["G" + (row + 2)].Value = dataResultProductsModel.Data[row].UnitPriceText;
                excelWorksheet.Cells["H" + (row + 2)].Value = dataResultProductsModel.Data[row].Description;
            }
            
            //Set the column width to the input width.
            excelWorksheet.Cells["A:H"].AutoFitColumns();
            excelWorksheet.Cells["A1:H1"].Style.Font.Bold = true;

            var excelData = excelPackage.GetAsByteArray();
            _httpContextAccessor.HttpContext.Response.Headers.Clear();
            _httpContextAccessor.HttpContext.Response.Clear();
            _httpContextAccessor.HttpContext.Response.ContentType =
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            _httpContextAccessor.HttpContext.Response.Headers.Add("content-length", excelData.Length.ToString());
            _httpContextAccessor.HttpContext.Response.Headers.Add("content-disposition",
                "attachment; filename= " + excelName + ".xlsx");
            await _httpContextAccessor.HttpContext.Response.Body.WriteAsync(excelData, 0, excelData.Length);
            _httpContextAccessor.HttpContext.Response.Body.Flush();
        }
    }

    private void UpdateFileExtension(BookDto dto, IFormFile file)
    {
        string oldExtension = string.IsNullOrWhiteSpace(dto.FilePath)
            ? null
            : Path.GetExtension(dto.FilePath);
        dto.FileExtension = file is not null && file.Length > 0
            ? Path.GetExtension(file.FileName)
            : oldExtension;
    }

    private bool? IsImagedToSaved(BookDto dto, IFormFile uploadedFile, bool isOverWrite = false)
    {
        //Validate the file
        bool? result = null;
        string fileName = null, fileExtension = null;
        if (uploadedFile is not null && uploadedFile.Length > 0)
        {
            result = false;
            fileName = uploadedFile.FileName;
            fileExtension = Path.GetExtension(fileName);
            string[] extensions = AppSettings.AcceptedImageExtensions.Split(",");
            
            //Checking if uploaded file extension is an accepted one or not.
            foreach (var extension in extensions)
            {
                if (fileExtension.ToLower() == extension.ToLower().Trim())
                {
                    result = true;
                    break;
                }
            }

            //If there is no problem with the extension, now checking the size of it.
            if (result == true)
            {
                double fileSize = AppSettings.AcceptedImageMaxSize * Math.Pow(1024, 2);
                if (uploadedFile.Length > fileSize)
                    result = false;
            }
        }

        //After the successful validations, streaming the file into the specific static folder
        if (result == true)
        {
            //Just file extension will be saved in the database but its id number and file extension in the static folder.
            fileName = dto.Id + fileExtension;
            string filePath = Path.Combine("wwwroot", "files", "books", fileName);
            using (FileStream fs = new FileStream(filePath, isOverWrite ? FileMode.Create : FileMode.CreateNew))
            {
                uploadedFile.CopyTo(fs);
            }
        }

        if (result == true)
            DeleteStaticFile(dto, fileExtension);

        return result;
    }

    private void DeleteStaticFile(BookDto dto, string uploadedExtension = null)
    {
        string oldExtension = string.IsNullOrWhiteSpace(dto.FilePath)
            ? null
            : Path.GetExtension(dto.FilePath);
        if (string.IsNullOrWhiteSpace(uploadedExtension) ||
            (!string.IsNullOrWhiteSpace(oldExtension) && oldExtension != uploadedExtension))
        {
            string filePath = Path.Combine("wwwroot", "files", "books", dto.Id + oldExtension);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }
    }
}