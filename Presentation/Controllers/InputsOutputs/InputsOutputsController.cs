using Core.Interface.Services.InputsOutputs;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers.InputsOutputs
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputsOutputsController : ControllerBase
    {
        private readonly IInputsOutputsService _inputsOutputsService;

        public InputsOutputsController(IInputsOutputsService inputsOutputsService)
        {
            _inputsOutputsService = inputsOutputsService;
        }

        [Middleware.Authorize]
        [HttpGet("GetAllInputsOutputsByProductId/{productId}")]
        public IActionResult GetAllInputsOutputsByProductId(int productId)
        {
            var response = _inputsOutputsService.GetAllInputsOutputsByProductId(productId);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpGet("GetAllInputsOutputsByUserId/{userId}")]
        public IActionResult GetAllInputsOutputsByUserId(int userId)
        {
            var response = _inputsOutputsService.GetAllInputsOutputsByUserId(userId);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpGet("GetAllInputsOutputs")]
        public IActionResult GetAllInputsOutputs()
        {
            var response = _inputsOutputsService.GetAllInputsOutputs();

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }
    }
}
