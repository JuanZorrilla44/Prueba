using Core.Dto.User;
using Core.Interface.Services.Login;
using Core.Interface.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        public UserController(IUserService userService, ILoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        [Middleware.Authorize("1")]
        [HttpGet("GetAllUserByStatus/{status}")]
        public IActionResult GetAllUserByStatus(int status)
        {
            var result = _userService.GetUsersAllByStatus(status);

            return result.Success ? Ok(result.Value) : BadRequest(result);
        }

        [Middleware.Authorize("1")]
        [HttpGet("GetUserByEmail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var result = _userService.GetUsersByEmail(email);

            return result.Success ? Ok(result.Value) : BadRequest(result);
        }

        [Middleware.Authorize("1")]
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetUsersById(id);
            
            return result.Success ? Ok(result.Value) : BadRequest(result);
        }

        [Middleware.Authorize("1")]
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserCreateDto userCreate)
        {
            var result = _userService.InsertUser(userCreate);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var result = _loginService.CreateToken(login);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Middleware.Authorize("1")]
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserUpdateDto userUpdate)
        {
            var result = _userService.UpdateUser(userUpdate);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Middleware.Authorize("1")]
        [HttpPut("UpdateStatusUser/{id}")]
        public IActionResult UpdateStatusUser(int id)
        {
            var result = _userService.ChangeStatusUSer(id);

            return result.Success ? Ok(result) : BadRequest(result);
        }


        [Middleware.Authorize("1")]
        [HttpPut("UpdateRolByUserId/{rolId}/{userId}")]
        public IActionResult UpdateRolByUserId(int rolId, int userId)
        {
            var result = _userService.ChangeRolUser(userId, rolId);

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
