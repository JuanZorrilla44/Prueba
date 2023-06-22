using Core.Dto.Product;

namespace Core.Interface.Services.Product
{
    public interface IProductService
    {
        ResponseService<string> CreateProduct(ProductCreateDto productCreate);
        ResponseService<string> UpdateProduct(ProductUpdateDto productUpdate);
        ResponseService<string> ChangeStatusProduct(int productId);
        ResponseService<IEnumerable<ProductEntity>> GetAllProductsByStatus(int status);
        ResponseService<IEnumerable<ProductEntity>> GetAllProductsByCategoryId(int categoryId);
        ResponseService<ProductEntity> GetProductById(int productId);
    }
}
