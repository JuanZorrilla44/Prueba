using Core.Dto.Category;
using Core.Interface.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Middleware.Authorize]
        [HttpGet("GetAllCategoryByStatus/{status}")]
        public IActionResult GetAllCategoryByStatus(int status)
        {
            var response = _categoryService.GetAllCategoriesByStatus(status);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpGet("GetCategoryById/{categoryId}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            var response = _categoryService.GetCategoryById(categoryId);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory(CategoryCreateDto categoryCreate)
        {
            var response = _categoryService.CreateCategory(categoryCreate);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory(CategoryUpdateDto categoryUpdate)
        {
            var response = _categoryService.UpdateCategory(categoryUpdate);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpPut("ChangeStatusCategory/{categoryId}")]
        public IActionResult ChangeStatusCategory(int categoryId)
        {
            var response = _categoryService.ChangeStatusCategory(categoryId);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpDelete("DeleteCategory/{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var response = _categoryService.DeleteCategory(categoryId);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
