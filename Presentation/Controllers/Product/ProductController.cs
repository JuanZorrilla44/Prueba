using Core.Dto.Product;
using Core.Interface.Services.Product;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Middleware.Authorize]
        [HttpGet("GetAllProductByCategoryId/{categoryId}")]
        public IActionResult GetAllProductByCategoryId(int categoryId)
        {
            var response = _productService.GetAllProductsByCategoryId(categoryId);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpGet("GetAllProductByStatus/{statusId}")]
        public IActionResult GetAllProductByStatus(int statusId)
        {
            var response = _productService.GetAllProductsByStatus(statusId);

            return response.Success ? Ok(response.Value) : BadRequest(response);
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(ProductCreateDto productCreateDto)
        {
            var response = _productService.CreateProduct(productCreateDto);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var response = _productService.UpdateProduct(productUpdateDto);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [Middleware.Authorize]
        [HttpPut("ChangeStatusProduct/{productId}")]
        public IActionResult ChangeStatusProduct(int productId)
        {
            var response = _productService.ChangeStatusProduct(productId);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
