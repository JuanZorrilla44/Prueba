using Core.Dto.User;
using Core.Entity;

namespace Core.Interface.Services.User
{
    public interface IUserService
    {
        ResponseService<IEnumerable<UserEntity>> GetUsersAllByStatus(int status);
        ResponseService<UserEntity> GetUsersByEmail(string email);
        ResponseService<UserEntity> GetUsersById(int userId);
        ResponseService<string> InsertUser(UserCreateDto user);
        ResponseService<string> UpdateUser(UserUpdateDto user);
        ResponseService<string> ChangeStatusUSer(int userId);
        ResponseService<string> ChangeRolUser(int userId, int rolId);
    }
}
