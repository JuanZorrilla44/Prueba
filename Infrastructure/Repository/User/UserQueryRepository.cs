using Core.Dto.User;
using Core.Entity;
using Infrastructure.Interface.Repository.User;

namespace Infrastructure.Repository.User
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersQuery _helpersQuery;

        public UserQueryRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersQuery helpersQuery)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersQuery = helpersQuery;
        }

        public IEnumerable<UserEntity> GetAllUserByStatus(int status)
        {
            string query = "SELECT * FROM Users WHERE StatusUser = @Status";
            IEnumerable<UserEntity> userEntities = _helpersQuery.ExecuteQuery<UserEntity, object>(query,
                new { @Status = status },
                _connectionStrings.ConnectionSqlServer!);
            return userEntities;
        }

        public IEnumerable<UserEntity> GetAllUsersByRolId(int roleId)
        {
            string query = "SELECT * FROM Users WHERE RoleId = @RoleId ";
            IEnumerable<UserEntity> userEntities = _helpersQuery.ExecuteQuery<UserEntity, object>(query,
                new { @RoleId = roleId },
                _connectionStrings.ConnectionSqlServer!);
            return userEntities;
        }

        public string? GetPassword(string email)
        {
            string query = "SELECT Password FROM Users WHERE Email = @Email";
            IEnumerable<string> password = _helpersQuery.ExecuteQuery<string, object>(query,
                new { @Email = email },
                _connectionStrings.ConnectionSqlServer!);
            return password.FirstOrDefault();
        }

        public UserEntity? GetUserByEmail(string email)
        {
            string query = "SELECT * FROM Users WHERE Email = @Email";
            IEnumerable<UserEntity> user = _helpersQuery.ExecuteQuery<UserEntity, object>(query,
               new { @Email = email },
               _connectionStrings.ConnectionSqlServer!);
            return user.FirstOrDefault();
        }

        public UserEntity? GetUserById(int id)
        {
            string query = "SELECT * FROM Users WHERE UserId = @UserId";
            IEnumerable<UserEntity> user = _helpersQuery.ExecuteQuery<UserEntity, object>(query,
               new { @UserId = id },
               _connectionStrings.ConnectionSqlServer!);
            return user.FirstOrDefault();
        }

        public UserEntity? GetUserByLogin(LoginDto login, string passwrodDescrip)
        {
            string query = "SELECT * FROM Users WHERE Email = @Email  and Password = @Password";
            IEnumerable<UserEntity> user = _helpersQuery.ExecuteQuery<UserEntity, object>(query,
               new
               {
                   login.Email,
                   @Password = passwrodDescrip
               },
               _connectionStrings.ConnectionSqlServer!);
            return user.FirstOrDefault();
        }
    }
}
