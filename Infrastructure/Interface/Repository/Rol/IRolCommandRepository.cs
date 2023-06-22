using Core.Dto.Rol;

namespace Infrastructure.Interface.Repository.Rol
{
    public interface IRolCommandRepository
    {

        ResponseDB CreateRol(RolCreateDto rolCreate);
        ResponseDB UpdateRol(RolUpdateDto rolUpdate);
        ResponseDB ChangeStatusRol(int rolId, int status);
    }
}
