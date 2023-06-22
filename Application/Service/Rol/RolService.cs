using Core.Dto.Rol;
using Core.Interface.Services.Rol;
using Infrastructure.Interface.Repository.Rol;
using Infrastructure.Interface.Repository.User;

namespace Application.Service.Rol
{
    public class RolService : IRolService
    {
        private readonly IRolQueryRepository _rolQueryRepository;
        private readonly IRolCommandRepository _rolCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;

        public RolService(IRolQueryRepository rolQueryRepository, IRolCommandRepository rolCommandRepository, IUserQueryRepository userQueryRepository)
        {
            _rolQueryRepository = rolQueryRepository;
            _rolCommandRepository = rolCommandRepository;
            _userQueryRepository = userQueryRepository;
        }

        public ResponseService<string> ChangeStatusRol(int roleId)
        {
            if (roleId <= 0)
            {
                return new ResponseService<string>
                {
                    Error = "La informacio no puedo llegar nula",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                RolesEntity? rol = _rolQueryRepository.GetRolById(roleId);

                if (rol == null)
                {
                    return new ResponseService<string>
                    {
                        Error = "El rol selecionado no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                IEnumerable<UserEntity> userEntities = _userQueryRepository.GetAllUsersByRolId(roleId);

                if (userEntities.Any())
                {
                    return new ResponseService<string>
                    {
                        Error = "El rol tiene usuarios asignados, no se permite cambiar de estado",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var response = _rolCommandRepository.ChangeStatusRol(roleId, rol.StatusRol ? 0 : 1);

                if (response.Success)
                {
                    return new ResponseService<string>
                    {
                        Value = response.Status,
                        Status = EStatusErrors.Succes,
                        Success = true,
                    };
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Error = response.Status,
                        Status = EStatusErrors.DB,
                        Success = true,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<string> CreateRol(RolCreateDto rolCreateDto)
        {
            if (string.IsNullOrEmpty(rolCreateDto.NameRol))
            {
                return new ResponseService<string>
                {
                    Error = "La informacio no puedo llegar nula",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _rolCommandRepository.CreateRol(rolCreateDto);

                if (response.Success)
                {
                    return new ResponseService<string>
                    {
                        Value = response.Status,
                        Status = EStatusErrors.Succes,
                        Success = true,
                    };
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Error = response.Status,
                        Status = EStatusErrors.DB,
                        Success = true,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<IEnumerable<RolesEntity>> GetAllRolesByStatus(int status)
        {
            if (status < 0 || status >= 2)
            {
                return new ResponseService<IEnumerable<RolesEntity>>
                {
                    Error = "La informacio no puedo llegar nula",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {

                var response = _rolQueryRepository.GetAllRolesByStatus(status);

                return new ResponseService<IEnumerable<RolesEntity>>
                {
                    Value = response,
                    Status = EStatusErrors.Succes,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<RolesEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<RolesEntity> GetRolByRolId(int rolId)
        {
            if (rolId <= 0)
            {
                return new ResponseService<RolesEntity>
                {
                    Error = "No puede llegar vacio",
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }

            try
            {
                RolesEntity? rol = _rolQueryRepository.GetRolById(rolId);

                if (rol == null)
                {
                    return new ResponseService<RolesEntity>
                    {
                        Error = "El rol selecionado no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }
                else
                {
                    return new ResponseService<RolesEntity>
                    {
                        Value = rol,
                        Status = EStatusErrors.Succes,
                        Success = true,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<RolesEntity>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<string> UpdateRol(RolUpdateDto rolUpdateDto)
        {
            if (rolUpdateDto.RoleId <= 0 || string.IsNullOrEmpty(rolUpdateDto.NameRol))
            {
                return new ResponseService<string>
                {
                    Error = "No puede llegar vacio",
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }

            try
            {
                RolesEntity? rol = _rolQueryRepository.GetRolById(rolUpdateDto.RoleId);

                if (rol == null)
                {
                    return new ResponseService<string>
                    {
                        Error = "El rol selecionado no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var response = _rolCommandRepository.UpdateRol(rolUpdateDto);

                if (response.Success)
                {
                    return new ResponseService<string>
                    {
                        Value = response.Status,
                        Status = EStatusErrors.Succes,
                        Success = true,
                    };
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Error = response.Status,
                        Status = EStatusErrors.DB,
                        Success = true,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
            throw new NotImplementedException();
        }
    }
}
