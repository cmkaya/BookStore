using System.Linq.Expressions;
using AppCore.Business.Results;
using Business.Abstracts;
using Business.Constants;
using Business.DataTransferObjects;
using DataAccess.Abstracts;
using Entities;

namespace Business.Concretes;

public class UserManager : IUserService
{
    private readonly BaseUserDal _userDal;

    public UserManager(BaseUserDal userDal)
    {
        _userDal = userDal;
    }

    public IQueryable<UserDto> Query()
    {
        return _userDal.Query("Role").Select(u => new UserDto
        {
            Id = u.Id,
            RoleId = u.RoleId,
            UserDetailId = u.UserDetailId,
            Guid = u.Guid,
            UserName = u.UserName,
            IsActive = u.IsActive,
            Password = u.Password,
            Role = new RoleDto
            {
                Id = u.Role.Id,
                Guid = u.Role.Guid,
                Name = u.Role.Name
            }
        });
    }

    public DataResult<List<UserDto>> GetAllUsers()
    {
        try
        {
            List<UserDto> dataResultUsers = Query().ToList();
            //TODO : dataResultUsers always false gosteriyor. Mantiki olarak dogru olabilir mi?
            if (dataResultUsers == null || dataResultUsers.Count == 0)
                return new ErrorDataResult<List<UserDto>>(Messages.NoUserFound);

            return new SuccessDataResult<List<UserDto>>(dataResultUsers);
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<List<UserDto>>(exc);
        }
    }

    public DataResult<UserDto> GetUserById(int id)
    {
        try
        {
            UserDto filteredUser = Query().SingleOrDefault(u => u.Id == id);
            if (filteredUser == null)
                return new ErrorDataResult<UserDto>(Messages.NoUserFound);

            return new SuccessDataResult<UserDto>(filteredUser);
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<UserDto>(exc);
        }
    }

    public DataResult<UserDto> GetFilteredUsers(Expression<Func<UserDto, bool>> predicate)
    {
        try
        {
            UserDto filteredUsers = Query().SingleOrDefault(predicate);
            if (filteredUsers == null)
                return new ErrorDataResult<UserDto>(Messages.NoUserFound);
    
            return new SuccessDataResult<UserDto>(filteredUsers);
        }
        catch (Exception exc)
        {
            return new ExceptionDataResult<UserDto>(exc);
        }
    }

    public Result Add(UserDto dto)
    {
        try
        {
            if (_userDal.Query().Any(u => u.UserName.ToLower() == dto.UserName.ToLower().Trim()))
                return new ErrorResult(Messages.ExistedUser);

            if (_userDal.Query("UserDetail")
                .Any(u => u.UserDetail.EMail.ToLower() == dto.UserDetail.EMail.ToLower().Trim()))
                return new ErrorResult(Messages.ExistedEmail);

            User userToAdd = new User
            {
                Guid = dto.Guid,
                UserName = dto.UserName.Trim(),
                IsActive = dto.IsActive,
                Password = dto.Password,
                RoleId = dto.RoleId,
                UserDetail = new UserDetail
                {
                    EMail = dto.UserDetail.EMail.Trim(),
                    CountryId = dto.UserDetail.CountryId,
                    CityId = dto.UserDetail.CityId,
                    Address = dto.UserDetail.Address.Trim()
                }
            };
            
            _userDal.Add(userToAdd);
            return new SuccessResult(Messages.UserAdded);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }

    public Result Update(UserDto dto)
    {
        if (_userDal.Query().Any(u => u.UserName.ToLower() == dto.UserName.ToLower().Trim() && u.Id != dto.Id))
            return new ErrorResult(Messages.ExistedUser);
        
        if (_userDal.Query("UserDetail").Any(u => u.UserDetail.EMail.ToLower() == dto.UserDetail.EMail.ToLower().Trim() && u.Id != dto.Id))
            return new ErrorResult(Messages.ExistedEmail);

        User userToUpdate = _userDal.Query().SingleOrDefault(u => u.Id == dto.Id);
        if (userToUpdate != null)
        {
            userToUpdate.Id = dto.Id;
            userToUpdate.RoleId = dto.RoleId;
            userToUpdate.UserDetailId = dto.UserDetailId;
            userToUpdate.UserName = dto.UserName;
            userToUpdate.Password = dto.Password;
            userToUpdate.IsActive = dto.IsActive;
            userToUpdate.UserDetail.EMail = dto.UserDetail.EMail.Trim();
            userToUpdate.UserDetail.CountryId = dto.UserDetail.CountryId;
            userToUpdate.UserDetail.CityId = dto.UserDetail.CityId;
            userToUpdate.UserDetail.Address = dto.UserDetail.Address.Trim();
        }
        
        _userDal.Update(userToUpdate);
        return new SuccessResult(Messages.UserUpdated);
    }

    public Result Delete(int id)
    {
        try
        {
            _userDal.Delete(id);
            return new SuccessResult(Messages.UserDeleted);
        }
        catch (Exception exc)
        {
            return new ExceptionResult(exc);
        }
    }
    
    public void Dispose()
    {
        _userDal.Dispose();
    }
}