using Core.Dto.User;
using Core.Entity;

namespace Infrastructure.Interface.Repository.User
{
    public interface IUserQueryRepository
    {
        IEnumerable<UserEntity> GetAllUserByStatus(int status);
        UserEntity? GetUserById(int id);
        UserEntity? GetUserByEmail(string email);
        UserEntity? GetUserByLogin(LoginDto login, string passwrodDescrip);
        string? GetPassword(string email);
        IEnumerable<UserEntity> GetAllUsersByRolId(int roleId);
    }
}
