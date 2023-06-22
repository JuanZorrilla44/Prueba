using Application.Service.Category;
using Application.Service.InputsOutputs;
using Application.Service.Login;
using Application.Service.Product;
using Application.Service.Rol;
using Application.Service.User;
using Core.Interface.Services.Category;
using Core.Interface.Services.InputsOutputs;
using Core.Interface.Services.Login;
using Core.Interface.Services.Product;
using Core.Interface.Services.Rol;
using Core.Interface.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class StartupApplication
    {
        public static void ConfigApp(this IServiceCollection service)
        {
            service.AddSingleton<IUserService, UserService>();
            service.AddSingleton<IJwtFactoryService, JwtFactoryService>();
            service.AddSingleton<ILoginService, LoginService>();
            service.AddSingleton<IRolService, RolService>();
            service.AddSingleton<ICategoryService, CategoryService>();
            service.AddSingleton<IProductService, ProductService>();
            service.AddSingleton<IInputsOutputsService, InputsOutputsService>();
        }
    }
}
