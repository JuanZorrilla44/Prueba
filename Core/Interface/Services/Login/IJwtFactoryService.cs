namespace Core.Interface.Services.Login
{
    public interface IJwtFactoryService
    {
        TokenEntity GenerateToken(UserEntity user);
    }
}
