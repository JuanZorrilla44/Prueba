using Core.Entity;
using Infrastructure.Interface.Repository.Rol;

namespace Infrastructure.Repository.Rol
{
    public class RolQueryRepository : IRolQueryRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersQuery _helpersQuery;

        public RolQueryRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersQuery helpersQuery)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersQuery = helpersQuery;
        }

        public IEnumerable<RolesEntity> GetAllRolesByStatus(int status)
        {
            string query = "SELECT * FROM Roles WHERE StatusRol = @Status";
            IEnumerable<RolesEntity> roles = _helpersQuery.ExecuteQuery<RolesEntity, object>(query,
                new { @Status = status },
                _connectionStrings.ConnectionSqlServer!);
            return roles;
        }

        public RolesEntity? GetRolById(int id)
        {
            string query = "SELECT * FROM Roles WHERE RoleId = @RoleId";
            IEnumerable<RolesEntity> rol = _helpersQuery.ExecuteQuery<RolesEntity, object>(query,
               new { @RoleId = id },
               _connectionStrings.ConnectionSqlServer!);
            return rol.FirstOrDefault();
        }
    }
}
