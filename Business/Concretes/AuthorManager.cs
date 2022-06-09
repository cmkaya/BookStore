using System.Globalization;
using AppCore.Business.Results;
using Business.Abstracts;
using Business.Constants;
using Business.DataTransferObjects;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes;

public class AuthorManager : IAuthorService
{
    private readonly BaseAuthorDal _authorDal;
    private readonly BaseBookDal _bookDal;

    public AuthorManager(BaseAuthorDal authorDal, BaseBookDal bookDal)
    {
        _authorDal = authorDal;
        _bookDal = bookDal;
    }

    public IQueryable<AuthorDto> Query()
    {
        return _authorDal.Query("Books").Select(a => new AuthorDto
        {
            Id = a.Id,
            Guid = a.Guid,
            FirstName = a.FirstName,
            LastName = a.LastName,
            FullNameDisplay = a.FirstName + " " + a.LastName,
            Birthdate = a.Birthdate,
            BirthdateText = a.Birthdate.HasValue ? a.Birthdate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",
            Bio = a.Bio,
            ShortBio = $"{a.Bio.Substring(0, 100)}..."
        });
    }

    public DataResult<List<AuthorDto>> GetAllAuthors(int? bookId = null)
    {
        try
        {
            var query = Query();
            if (bookId != null)
                query = query.Where(a => a.Id == bookId);

            return new SuccessDataResult<List<AuthorDto>>(query.ToList());
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<List<AuthorDto>>(exc);
        }    
    }

    public async Task<DataResult<List<AuthorDto>>> GetAuthorsAsync()
    {
        try
        {
            var entityAuthors = await _authorDal.Query().ToListAsync();
            var dtoAuthors = entityAuthors.Select(a => new AuthorDto
            {
                Id = a.Id,
                FullNameDisplay = a.FirstName + " " + a.LastName
            }).ToList();

            return new SuccessDataResult<List<AuthorDto>>(dtoAuthors);
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<List<AuthorDto>>(exc);
        }
    }

    public Result Add(AuthorDto dto)
    {
        try
        {
            if (_authorDal.Query().Any(a =>
                    a.FirstName.ToLower() == dto.FirstName.ToLower().Trim() &&
                    a.LastName.ToLower() == dto.LastName.ToLower().Trim()))
                return new ErrorResult(Messages.ExistedAuthor);

            var authorToCreate = new Author
            {
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                // DateTime = string.IsNullOrWhiteSpace(model.BirthdateText)
                //     ? null
                //     : DateTime.Parse(model.BirthdateText, new CultureInfo("en-US")),
                Birthdate = dto.Birthdate,
                Bio = dto.Bio?.Trim()
            };
            
            _authorDal.Add(authorToCreate);
            return new SuccessResult(Messages.AuthorAdded);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public Result Update(AuthorDto dto)
    {
        try
        {
            if (_authorDal.Query().Any(a =>
                    (a.FirstName.ToLower() == dto.FirstName &&
                     a.LastName.ToLower() == dto.LastName.ToLower().Trim()) && a.Id != dto.Id))
                return new ErrorResult(Messages.ExistedAuthor);
            
            Author authorToUpdate = _authorDal.Query().SingleOrDefault(a => a.Id == dto.Id);
            if (authorToUpdate != null)
            {
                authorToUpdate.FirstName = dto.FirstName.Trim();
                authorToUpdate.LastName = dto.LastName.Trim();
                authorToUpdate.Birthdate = string.IsNullOrWhiteSpace(dto.BirthdateText)
                    ? null
                    : DateTime.Parse(dto.BirthdateText, new CultureInfo("en-US"));
                authorToUpdate.Bio = dto.Bio?.Trim();
            }
            
            _authorDal.Update(authorToUpdate);
            return new SuccessResult(Messages.AuthorUpdated);
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
            var authorToDelete = _authorDal.Query(a => a.Id == id, "Books").SingleOrDefault();
            if (authorToDelete != null)
            {
                if (authorToDelete.Books.Any())
                    return new ErrorResult(Messages.InvalidAuthorRemove);
            }
            
            _authorDal.Delete(id);
            return new SuccessResult(Messages.AuthorDeleted);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public void Dispose()
    {
        _authorDal.Dispose();
    }
}