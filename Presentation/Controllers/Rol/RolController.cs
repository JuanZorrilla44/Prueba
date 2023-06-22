using Core.Dto.Rol;
using Core.Interface.Services.Rol;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers.Rol
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }


        [Middleware.Authorize("1")]
        [HttpGet("GetAllRolesByStatus/{status}")]
        public IActionResult GetAllRolesByStatus(int status)
        {
            var response = _rolService.GetAllRolesByStatus(status);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }


        [Middleware.Authorize("1")]
        [HttpGet("GetRolByRolId/{rolId}")]
        public IActionResult GetRolByRolId(int rolId)
        {
            var response = _rolService.GetRolByRolId(rolId);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }


        [Middleware.Authorize("1")]
        [HttpPost("CreateRol")]
        public IActionResult CreateRol(RolCreateDto rolCreate)
        {
            var response = _rolService.CreateRol(rolCreate);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [Middleware.Authorize("1")]
        [HttpPut("UpdateRol")]
        public IActionResult UpdateRol(RolUpdateDto rolUpdate)
        {
            var response = _rolService.UpdateRol(rolUpdate);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [Middleware.Authorize("1")]
        [HttpPut("ChangeStatusRol/{id}")]
        public IActionResult ChangeStatusRol(int id)
        {
            var response = _rolService.ChangeStatusRol(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
