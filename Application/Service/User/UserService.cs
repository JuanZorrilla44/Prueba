using Core.Dto.User;
using Core.Interface.Services.User;
using Infrastructure.Interface.Repository.User;

namespace Application.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly UtilsSingleton _utilsSingleton;

        public UserService(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository)
        {
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _utilsSingleton = UtilsSingleton.Instance;
        }

        public ResponseService<string> ChangeRolUser(int userId, int rolId)
        {
            if (userId <= 0 || rolId <= 0)
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
                UserEntity? user = _userQueryRepository.GetUserById(userId);

                if (user == null)
                {
                    return new ResponseService<string>
                    {
                        Error = "El usuario no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var response = _userCommandRepository.ChangeRolUser(userId, rolId);

                if (response.Success)
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = response.Status
                    };
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.DB,
                        Success = false,
                        Error = response.Status
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

        public ResponseService<string> ChangeStatusUSer(int userId)
        {
            if (userId <= 0)
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
                UserEntity? user = _userQueryRepository.GetUserById(userId);

                if (user == null)
                {
                    return new ResponseService<string>
                    {
                        Error = "El usuario no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var response = _userCommandRepository.ChangeUserStatus(userId, user.StatusUser ? 0 : 1);

                return new ResponseService<string>
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = response.Status
                };

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

        public ResponseService<IEnumerable<UserEntity>> GetUsersAllByStatus(int status)
        {
            if (status < 0 || status >= 2)
            {
                return new ResponseService<IEnumerable<UserEntity>>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var result = _userQueryRepository.GetAllUserByStatus(status).OrderBy(x => x.UserId);

                return new ResponseService<IEnumerable<UserEntity>>
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = result 
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<UserEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<UserEntity> GetUsersByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new ResponseService<UserEntity>
                {
                    Error = "El email es requerido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }
            if (!_utilsSingleton.ValidateEmail(email))
            {
                return new ResponseService<UserEntity>
                {
                    Error = "El email es requerido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var result = _userQueryRepository.GetUserByEmail(email);

                if (result == null)
                {
                    return new ResponseService<UserEntity>
                    {
                        Error = "El usuario no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                return new ResponseService<UserEntity>
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<UserEntity>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<UserEntity> GetUsersById(int userId)
        {
            if (userId < 0)
            {
                return new ResponseService<UserEntity>
                {
                    Error = "La informacio no puedo llegar nula",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                UserEntity? result = _userQueryRepository.GetUserById(userId);

                if (result == null)
                {
                    return new ResponseService<UserEntity>
                    {
                        Error = "El usuario no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                return new ResponseService<UserEntity>
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<UserEntity>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<string> InsertUser(UserCreateDto user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.FullName) 
                || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Email)
                || user.RoleId <= 0)
            {
                return new ResponseService<string>
                {
                    Error = "El error con la informacion",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                if (long.Parse(user.Phone) <= 0)
                {
                    return new ResponseService<string>
                    {
                        Error = "el numero no puede venir negativo y ni cero",
                        Status = EStatusErrors.Data,
                        Success = false,
                    };
                }

                var result = _userCommandRepository.InsertUser(user, _utilsSingleton.Encrypt(user.Password), long.Parse(user.Phone));
                if (result.Success)
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = result.Status
                    };
                }else
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.DB,
                        Success = false,
                        Error = result.Status
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string> { Error = ex.Message, Status = EStatusErrors.Failed, Success = false, };
            }
        }

        public ResponseService<string> UpdateUser(UserUpdateDto user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.FullName)
               || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Email)
               || user.UserId <= 0)
            {
                return new ResponseService<string>
                {
                    Error = "El error con la informacion",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                if (long.Parse(user.Phone) <= 0)
                {
                    return new ResponseService<string>
                    {
                        Error = "el numero no puede venir negativo y ni cero",
                        Status = EStatusErrors.Data,
                        Success = false,
                    };
                }

                var result = _userCommandRepository.UpdateUser(user, long.Parse(user.Phone));
                if (result.Success)
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = result.Status
                    };
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.DB,
                        Success = false,
                        Error = result.Status
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string> { Error = ex.Message, Status = EStatusErrors.Failed, Success = false, };
            }
        }
    }
}
