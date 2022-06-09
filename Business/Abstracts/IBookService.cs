using AppCore.Business.Paging;
using AppCore.Business.Results;
using AppCore.Business.Services;
using Business.DataTransferObjects;
using Business.DataTransferObjects.FilterAndPaging;

namespace Business.Abstracts;

public interface IBookService : IServiceCore<BookDto>
{
    DataResult<List<BookDto>> GetBooksByFilter(FilteredBookDto filterBy = null, PageInfo pageInfo = null);
}