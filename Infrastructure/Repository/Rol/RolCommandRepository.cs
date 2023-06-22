using Core.Dto.Rol;
using Infrastructure.Interface.Repository.Rol;

namespace Infrastructure.Repository.Rol
{
    public class RolCommandRepository : IRolCommandRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersCommand _helpersCommand;

        public RolCommandRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersCommand helpersCommand)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersCommand = helpersCommand;
        }

        public ResponseDB ChangeStatusRol(int rolId, int status)
        {
            string commandUpdate = "UPDATE Roles SET StatusRol = @Status WHERE RoleId = @RoleId";
            int execute = _helpersCommand.ExecuteCommad<object>(
                commandUpdate,
                new { @Status = status, RoleId = rolId },
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al cambiar el estado del rol" : "Error al cambiar el estado del rol",
                Success = execute > 0,
            };
        }

        public ResponseDB CreateRol(RolCreateDto rolCreate)
        {
            string commandCreate = "INSERT INTO Roles (NameRol) VALUES (@NameRol); SELECT CAST(SCOPE_IDENTITY() as int)";
            int execute = _helpersCommand.ExecuteCommad(
                commandCreate,
                rolCreate,
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Insert.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al insertar informacio en la tabla de roles" : "Error al insertar la informacion",
                Success = execute > 0,
            };
        }

        public ResponseDB UpdateRol(RolUpdateDto rolUpdate)
        {
             string commandUpdate = "UPDATE Roles SET NameRol = @NameRol WHERE RoleId = @RoleId;";
            int execute = _helpersCommand.ExecuteCommad(
                commandUpdate,
                rolUpdate,
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al cambiar la informacio en la tabla de roles" : "Error al actualizar la informacion",
                Success = execute > 0,
            };
        }
    }
}
