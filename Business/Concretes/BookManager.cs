using System.Globalization;
using AppCore.Business.Paging;
using AppCore.Business.Results;
using Business.Abstracts;
using Business.Constants;
using Business.DataTransferObjects;
using Business.DataTransferObjects.FilterAndPaging;
using DataAccess.Abstracts;
using Entities;

namespace Business.Concretes;

public class BookManager : IBookService
{
    private readonly BaseBookDal _bookDal;

    public BookManager(BaseBookDal bookDal)
    {
        _bookDal = bookDal;
    }

    public IQueryable<BookDto> Query()
    {
        return _bookDal.Query("Author").Select(b => new BookDto
        {
            Id = b.Id,
            AuthorId = b.AuthorId,
            Guid = b.Guid,
            Name = b.Name,
            Description = b.Description,
            StockAmount = b.StockAmount,
            Page = b.Page,
            Publisher = b.Publisher,
            PublicationDate = b.PublicationDate,
            PublicationDateText = b.PublicationDate.HasValue ? b.PublicationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",
            UnitPrice = b.UnitPrice,
            UnitPriceText = b.UnitPrice.ToString(new CultureInfo("en-US")),
            ShortDescription = $"{b.Description.Substring(0, 100)}...",
            FileExtension = b.FileExtension,
            FilePath = string.IsNullOrWhiteSpace(b.FileExtension) 
                ? null
                : $"/files/books/{b.Id}{b.FileExtension}",
            Author = new AuthorDto
            {
                Id = b.Id,
                FirstName = b.Author.FirstName,
                LastName = b.Author.LastName,
                BirthdateText = b.Author.Birthdate.HasValue ? b.Author.Birthdate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",
                Bio = b.Author.Bio,
                FullNameDisplay = b.Author.FirstName + " " + b.Author.LastName
            }
        });
    }

    public Result Add(BookDto dto)
    {
        try
        {
            if (_bookDal.Query().Any(b => b.Name.ToLower() == dto.Name.ToLower().Trim()))
                return new ErrorResult(Messages.ExistedBook);

            var bookToAdd = new Book
            {
                AuthorId = dto.AuthorId,
                Guid = dto.Guid,
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim(),
                StockAmount = dto.StockAmount,
                Page = dto.Page,
                Publisher = dto.Publisher?.Trim(),
                PublicationDate = string.IsNullOrWhiteSpace(dto.PublicationDateText)
                    ? null
                    : DateTime.Parse(dto.PublicationDateText, new CultureInfo("en-US")),
                UnitPrice = Convert.ToDecimal(dto.UnitPriceText.Replace(",", "."), new CultureInfo("en-US")),
                FileExtension = dto.FileExtension
            };
            
            _bookDal.Add(bookToAdd);
            dto.Id = bookToAdd.Id;
            return new SuccessResult(Messages.BookAdded);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public DataResult<List<BookDto>> GetBooksByFilter(FilteredBookDto filterBy = null, PageInfo pageInfo = null)
    {
        try
        {
            var query = Query();

            if (filterBy is not null)
            {
                var authorId = filterBy.AuthorId;
                
                //Filter by author id
                if (authorId is not null)
                    query = query.Where(b => b.AuthorId == authorId);

                //Filter by book name
                if (!string.IsNullOrWhiteSpace(filterBy.BookName))
                    query = query.Where(q => q.Name.ToLower().Contains(filterBy.BookName.ToLower().Trim()));

                //Filter by unit price
                if (!string.IsNullOrWhiteSpace(filterBy.BeginningPriceText))
                {
                    decimal temp = Convert.ToDecimal(filterBy.BeginningPriceText.Trim().Replace(",", "."),
                        new CultureInfo("en-US"));
                    query = query.Where(q => q.UnitPrice >= temp);
                }

                if (!string.IsNullOrWhiteSpace(filterBy.EndPriceText))
                {
                    decimal temp = Convert.ToDecimal(filterBy.EndPriceText.Trim().Replace(",", "."),
                        new CultureInfo("en-US"));
                    query = query.Where(q => q.UnitPrice <= temp);
                }

                //Filter by stock amount
                if (filterBy.BeginningStockAmount.HasValue)
                    query = query.Where(q => q.StockAmount >= filterBy.BeginningStockAmount);

                if (filterBy.EndStockAmount.HasValue)
                    query = query.Where(q => q.StockAmount <= filterBy.EndStockAmount);

                //Filter by date
                DateTime tempBeginningDate, tempEndDate;
                if (!string.IsNullOrWhiteSpace(filterBy.BeginningPublicationDateText))
                {
                    tempBeginningDate = DateTime.Parse(filterBy.BeginningPublicationDateText, new CultureInfo("en-US"));
                    query = query.Where(q => q.PublicationDate >= tempBeginningDate);
                }

                if (!string.IsNullOrWhiteSpace(filterBy.EndPublicationDateText))
                {
                    tempEndDate = DateTime.Parse(filterBy.EndPublicationDateText, new CultureInfo("en-US"));
                    query = query.Where(q => q.PublicationDate <= tempEndDate);
                }
            }

            //Paging
            if (pageInfo is not null)
            {
                pageInfo.TotalRecord = query.Count();
                query = query.Skip((pageInfo.CurrentPage - 1) * pageInfo.RecordPerPage).Take(pageInfo.RecordPerPage);
            }
            
            return new SuccessDataResult<List<BookDto>>(query.ToList());
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<List<BookDto>>(exc);
        }
    }

    //public DataResult<List<BookSearchDto>> GetBooksByFilter(Book)
    public Result Update(BookDto dto)
    {
        try
        {
            if (_bookDal.Query().Any(b => b.Name.ToLower() == dto.Name.ToLower().Trim() && b.Id != dto.Id))
                return new ErrorResult(Messages.ExistedBook);

            var bookToUpdate = _bookDal.Query().SingleOrDefault(b => b.Id == dto.Id);
            if (bookToUpdate != null)
            {
                bookToUpdate.AuthorId = dto.AuthorId;
                bookToUpdate.Guid = dto.Guid;
                bookToUpdate.Name = dto.Name.Trim();
                bookToUpdate.Description = dto.Description?.Trim();
                bookToUpdate.StockAmount = dto.StockAmount;
                bookToUpdate.Page = dto.Page;
                bookToUpdate.Publisher = dto.Publisher?.Trim();
                bookToUpdate.PublicationDate = string.IsNullOrWhiteSpace(dto.PublicationDateText)
                    ? null
                    : DateTime.Parse(dto.PublicationDateText, new CultureInfo("en-US"));
                bookToUpdate.UnitPrice = Convert.ToDecimal(dto.UnitPriceText.Replace(",", "."), new CultureInfo("en-US"));
                bookToUpdate.FileExtension = dto.FileExtension;
            }
            
            _bookDal.Update(bookToUpdate);
            return new SuccessResult(Messages.BookUpdated);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public Result Delete(int id)
    {
        try
        {
            _bookDal.Delete(id);
            return new SuccessResult(Messages.BookDeleted);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }
    
    public void Dispose()
    {
        _bookDal.Dispose();
    }
}