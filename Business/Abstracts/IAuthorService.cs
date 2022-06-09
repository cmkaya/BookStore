using AppCore.Business.Results;
using AppCore.Business.Services;
using Business.DataTransferObjects;

namespace Business.Abstracts;

public interface IAuthorService : IServiceCore<AuthorDto>
{
    DataResult<List<AuthorDto>> GetAllAuthors(int? bookId = null);
    Task<DataResult<List<AuthorDto>>> GetAuthorsAsync();
}