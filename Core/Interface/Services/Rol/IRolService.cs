using Core.Dto.Rol;

namespace Core.Interface.Services.Rol
{
    public interface IRolService
    {
        ResponseService<string> CreateRol(RolCreateDto rolCreateDto);
        ResponseService<string> UpdateRol(RolUpdateDto rolUpdateDto);
        ResponseService<string> ChangeStatusRol(int roleId);
        ResponseService<IEnumerable<RolesEntity>> GetAllRolesByStatus(int status);
        ResponseService<RolesEntity> GetRolByRolId(int rolId);
    }
}
