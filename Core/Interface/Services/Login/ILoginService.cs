using Core.Dto.User;

namespace Core.Interface.Services.Login
{
    public interface ILoginService
    {
        ResponseService<LoginEntity> CreateToken(LoginDto login);
    }
}
