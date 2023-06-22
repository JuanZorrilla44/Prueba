using Core.Dto.User;
using Infrastructure.Interface.Repository.User;

namespace Infrastructure.Repository.User
{
    public class UserCommandRepository: IUserCommandRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersCommand _helpersCommand;

        public UserCommandRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersCommand helpersCommand)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersCommand = helpersCommand;
        }

        public ResponseDB ChangeRolUser(int userId, int rolUserId)
        {
            string commandUpdate = "UPDATE Users SET RoleId = @RoleId WHERE UserId = @UserId";
            int execute = _helpersCommand.ExecuteCommad<object>(
                commandUpdate,
                new { @RoleId = rolUserId, UserId = userId },
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al cambiar el estado del usuario" : "Error al cambiar el estado del usuario",
                Success = execute > 0,
            };
        }

        public ResponseDB ChangeUserStatus(int userId, int status)
        {
            string commandUpdate = "UPDATE Users SET StatusUser = @Status WHERE UserId = @userId";
            int execute = _helpersCommand.ExecuteCommad<object>(
                commandUpdate,
                new { @Status = status, UserId = userId },
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Update.GetStringValue());
            return new ResponseDB() { 
                Status = execute > 0 ? "Exito al cambiar el estado del usuario" : "Error al cambiar el estado del usuario",
                Success = execute > 0,
            };
        }

        public ResponseDB InsertUser(UserCreateDto userCreate, string passwordEncryted, long phoneNumber)
        {

            string commandUpdate = "INSERT INTO Users (Email, FullName, Password, Phone, RoleId, UserName) VALUES (@Email, @FullName, @Password, @Phone, @RoleId, @UserName ); SELECT CAST(SCOPE_IDENTITY() as int)";
            int execute = _helpersCommand.ExecuteCommad<object>(
                commandUpdate,
                 new
                 {
                     userCreate.UserName,
                     userCreate.FullName,
                     userCreate.Email,
                     userCreate.RoleId,
                     @Password = passwordEncryted,
                     @Phone = phoneNumber
                 },
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Insert.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al insertar informacio en la tabla de usuarios" : "Error al insertar la informacion",
                Success = execute > 0,
            };
        }

        public ResponseDB UpdateUser(UserUpdateDto userUpdate, long phoneNumber)
        {
            string commandUpdate = "UPDATE Users SET FullName = @FullName, Email = @Email,Phone = @phone, UserName = @UserName  WHERE UserId = @userId;";
            int execute = _helpersCommand.ExecuteCommad<object>(
                commandUpdate,
                new
                {
                    userUpdate.UserName,
                    userUpdate.FullName,
                    userUpdate.Email,
                    userUpdate.UserId,
                    @phone = phoneNumber
                },
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al cambiar la informacio en la tabla de usuarios" : "Error al actualizar la informacion",
                Success = execute > 0,
            };
        }
    }
}
