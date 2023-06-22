using Core.Entity;
using Infrastructure.Interface.Repository.Product;

namespace Infrastructure.Repository.Product
{
    internal class ProductQueryRepository : IProductQueryRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersQuery _helpersQuery;

        public ProductQueryRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersQuery helpersQuery)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersQuery = helpersQuery;
        }

        public IEnumerable<ProductEntity> GetAllProductsByCategoryId(int categoryId)
        {
            string query = """
                            SELECT P.CategoryId, P.NameProduct, P.Price, P.ProductId, P.Quantity, P.StatusProduct, C.NameCategory 
                            FROM Products P
                            INNER JOIN Categories C ON P.CategoryId = C.CategoryID
                            WHERE P.CategoryId = @CategoryId
                            """;
            IEnumerable<ProductEntity> products = _helpersQuery.ExecuteQuery<ProductEntity, object>(query,
                new { @CategoryId = categoryId },
                _connectionStrings.ConnectionSqlServer!);
            return products;
        }

        public IEnumerable<ProductEntity> GetAllProductsByStatus(int status)
        {
            string query = """
                            SELECT P.CategoryId, P.NameProduct, P.Price, P.ProductId, P.Quantity, P.StatusProduct, C.NameCategory 
                            FROM Products P
                            INNER JOIN Categories C ON P.CategoryId = C.CategoryID
                            WHERE P.StatusProduct = @Status
                            """;
            IEnumerable<ProductEntity> products = _helpersQuery.ExecuteQuery<ProductEntity, object>(query,
                new { @Status = status },
                _connectionStrings.ConnectionSqlServer!);
            return products;
        }

        public ProductEntity? GetProductById(int productId)
        {
            string query = """
                            SELECT P.CategoryId, P.NameProduct, P.Price, P.ProductId, P.Quantity, P.StatusProduct, C.NameCategory 
                            FROM Products P
                            INNER JOIN Categories C ON P.CategoryId = C.CategoryID
                            WHERE P.ProductId = @ProductId
                            """;
            IEnumerable<ProductEntity> products = _helpersQuery.ExecuteQuery<ProductEntity, object>(query,
                new { @ProductId = productId },
                _connectionStrings.ConnectionSqlServer!);
            return products.FirstOrDefault();
        }
    }
}
