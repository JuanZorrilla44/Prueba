using Core.Dto.Product;

namespace Infrastructure.Interface.Repository.Product
{
    public interface IProductCommandRepository
    {
        ResponseDB CreateProduct(ProductCreateDto productCreate);
        ResponseDB UpdateProduct(ProductUpdateDto productUpdate);
        ResponseDB ChangeStatusProduc(int productId,  int status);
    }
}
