using Core.Entity;

namespace Infrastructure.Interface.Repository.Product
{
    public interface IProductQueryRepository
    {
        IEnumerable<ProductEntity> GetAllProductsByStatus(int status);
        IEnumerable<ProductEntity> GetAllProductsByCategoryId(int categoryId);
        ProductEntity? GetProductById(int productId); 
    }
}
