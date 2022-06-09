using AppCore.Business.Results;
using Business.DataTransferObjects;

namespace Business.Abstracts;

public interface IAccountService
{
    DataResult<UserDto> Login(UserLoginDto dto);
    Result Register(UserRegisterDto dto);
}