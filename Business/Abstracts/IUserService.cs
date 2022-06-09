using System.Linq.Expressions;
using AppCore.Business.Results;
using AppCore.Business.Services;
using Business.DataTransferObjects;

namespace Business.Abstracts;

public interface IUserService : IServiceCore<UserDto>
{
    DataResult<List<UserDto>> GetAllUsers();
    DataResult<UserDto> GetUserById(int id);
    DataResult<UserDto> GetFilteredUsers(Expression<Func<UserDto, bool>> predicate);
}