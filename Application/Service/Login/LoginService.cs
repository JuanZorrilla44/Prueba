using Core.Dto.User;
using Core.Interface.Services.Login;
using Infrastructure.Interface.Repository.User;

namespace Application.Service.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IJwtFactoryService _jwtFactoryService;
        private readonly UtilsSingleton _utilsSingleton;

        public LoginService(IUserQueryRepository userQueryRepository, IJwtFactoryService jwtFactoryService)
        {
            _userQueryRepository = userQueryRepository;
            _jwtFactoryService = jwtFactoryService;
            _utilsSingleton = UtilsSingleton.Instance;
        }

        public ResponseService<LoginEntity> CreateToken(LoginDto login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return new ResponseService<LoginEntity>()
                {
                    Error = "Error con la informacion",
                    Status = EStatusErrors.Data,
                    Success = false
                };
            }

            try
            {
                string? password = _userQueryRepository.GetPassword(login.Email);

                if (string.IsNullOrEmpty(password))
                {
                    return new ResponseService<LoginEntity>()
                    {
                        Error = "Correo o contraseña equivocada",
                        Status = EStatusErrors.DB,
                        Success = false
                    };
                }

                string descrip = _utilsSingleton.Decrypt(password);

                if (login.Password == descrip)
                {
                    UserEntity? userEntity = _userQueryRepository.GetUserByEmail(login.Email);

                    if (userEntity == null)
                    {
                        return new ResponseService<LoginEntity>()
                        {
                            Error = "Correo o contraseña equivocada",
                            Status = EStatusErrors.DB,
                            Success = false
                        };
                    }
                    else
                    {
                        if (userEntity.StatusUser)
                        {
                            TokenEntity token = _jwtFactoryService.GenerateToken(userEntity);
                            return new ResponseService<LoginEntity>()
                            {
                                Status = EStatusErrors.Succes,
                                Success = true,
                                Value = new LoginEntity()
                                {
                                    AuthToken = token.AuthToken,
                                    ExpiresIn = token.ExpiresIn,
                                    LoginInfo = new LoginInfo()
                                    {
                                        Email = userEntity.Email,
                                        FullName = userEntity.FullName,
                                        Phone = userEntity.Phone,
                                        RoleId = userEntity.RoleId,
                                        StatusUser = userEntity.StatusUser,
                                        UserId = userEntity.UserId,
                                        UserName = userEntity.UserName
                                    }
                                }
                            };
                        }
                        else
                        {
                            return new ResponseService<LoginEntity>()
                            {
                                Error = "El usuario no esta activo",
                                Status = EStatusErrors.DB,
                                Success = false
                            };
                        }
                    }
                }
                else
                {
                    return new ResponseService<LoginEntity>()
                    {
                        Error = "Correo o contraseña equivocada",
                        Status = EStatusErrors.DB,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseService<LoginEntity>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }
    }
}
