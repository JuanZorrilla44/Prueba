using Core.Dto.User;

namespace Infrastructure.Interface.Repository.User
{
    public interface IUserCommandRepository
    {
        ResponseDB InsertUser(UserCreateDto userCreate, string passwordEncryted, long phoneNumber);
        ResponseDB UpdateUser(UserUpdateDto userUpdate, long phoneNumber);
        ResponseDB ChangeUserStatus(int userId, int status);
        ResponseDB ChangeRolUser(int userId, int rolUserId);
    }
}
