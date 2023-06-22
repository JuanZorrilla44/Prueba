using Core.Entity;

namespace Infrastructure.Interface.Repository.Rol
{
    public interface IRolQueryRepository
    {
        IEnumerable<RolesEntity> GetAllRolesByStatus(int status);
        RolesEntity? GetRolById(int id);
    }
}
