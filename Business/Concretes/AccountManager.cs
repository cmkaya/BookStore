using AppCore.Business.Results;
using Business.Abstracts;
using Business.DataTransferObjects;
using Business.Enums;

namespace Business.Concretes;

public class AccountManager : IAccountService
{
    private readonly IUserService _userService;

    public AccountManager(IUserService userService)
    {
        _userService = userService;
    }

    public DataResult<UserDto> Login(UserLoginDto dto)
    {
        try
        {
            DataResult<UserDto> userDataResult = _userService.GetFilteredUsers(u =>
                u.UserName.ToLower().Equals(dto.UserName.ToLower()) && u.Password.ToLower().Equals(dto.Password.ToLower()) &&
                u.IsActive);
            
            return userDataResult;
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<UserDto>(exc);
        }
    }

    public Result Register(UserRegisterDto dto)
    {
        try
        {
            UserDto user = new UserDto
            {
                Guid = dto.Guid,
                UserName = dto.UserName.Trim(),
                Password = dto.Password.Trim(),
                IsActive = true,
                RoleId = (int) Role.User,
               
                UserDetail = new UserDetailDto
                {
                    EMail = dto.UserDetail.EMail.Trim(),
                    CountryId = dto.UserDetail.CountryId,
                    CityId = dto.UserDetail.CityId,
                    Address = dto.UserDetail.Address.Trim()
                }
            };

            return _userService.Add(user);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }
}