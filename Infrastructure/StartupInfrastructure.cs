using Infrastructure.Helpers;
using Infrastructure.Interface.Repository.Category;
using Infrastructure.Interface.Repository.InputsOutputs;
using Infrastructure.Interface.Repository.Product;
using Infrastructure.Interface.Repository.Rol;
using Infrastructure.Interface.Repository.User;
using Infrastructure.Repository.Category;
using Infrastructure.Repository.InputsOutputs;
using Infrastructure.Repository.Product;
using Infrastructure.Repository.Rol;
using Infrastructure.Repository.User;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class StartupInfrastructure
    {
        public static void ConfigInfrastructure(this IServiceCollection service)
        {
            service.AddSingleton<IHelpersCommand, HelpersCommand>();
            service.AddSingleton<IHelpersQuery, HelpersQuery>();
            service.AddSingleton<IUserCommandRepository, UserCommandRepository>();
            service.AddSingleton<IUserQueryRepository, UserQueryRepository>();
            service.AddSingleton<IRolCommandRepository, RolCommandRepository>();
            service.AddSingleton<IRolQueryRepository, RolQueryRepository>();
            service.AddSingleton<ICategoryQueryRepository, CategoryQueryRepository>();
            service.AddSingleton<ICategoryCommandRepository, CategoryCommandRepository>();
            service.AddSingleton<IProductCommandRepository, ProductCommandRepository>();
            service.AddSingleton<IProductQueryRepository, ProductQueryRepository>();
            service.AddSingleton<IInputsOutputsCommandRepository, InputsOutputsCommandRepository>();
            service.AddSingleton<IInputsOutputsQueryRepository, InputsOutputsQueryRepository>();
        }
    }
}
