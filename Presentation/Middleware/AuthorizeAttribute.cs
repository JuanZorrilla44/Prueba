using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.Entity;
using Core.Response;

namespace Presentation.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        private readonly string? _Rol;
        public AuthorizeAttribute() { }
        public AuthorizeAttribute(string? rol)
        {
            _Rol = rol;
        }
        /// <summary>
        /// Validate if the context exists a user, in case it does not exist returns unauthorized
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult
                 (new ResponseService<string>()
                 {
                     Error = "No tienes acceso a esta url",
                     Status = EStatusErrors.Authorization,
                     Success = false
                 })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (!string.IsNullOrEmpty(_Rol))
                {
                    UserEntity userEntity = (UserEntity)user;
                    if(userEntity.RoleId != int.Parse(_Rol))
                    {
                        context.Result = new JsonResult
               (new ResponseService<string>()
               {
                   Error = "Este rol no tiene acceso a esta url",
                   Status = EStatusErrors.AuthorizationRol,
                   Success = false
               })
                        { StatusCode = StatusCodes.Status406NotAcceptable };
                    }
                }
            }
        }
    }
}
