using Core.Entity;
using Infrastructure.Interface.Repository.Category;

namespace Infrastructure.Repository.Category
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersQuery _helpersQuery;

        public CategoryQueryRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersQuery helpersQuery)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersQuery = helpersQuery;
        }

        public IEnumerable<CategoryEntity> GetAllCategoriesByStatus(int status)
        {
            string query = "SELECT * FROM Categories WHERE StatusCategory = @Status";
            IEnumerable<CategoryEntity> categories = _helpersQuery.ExecuteQuery<CategoryEntity, object>(query,
                new { @Status = status },
                _connectionStrings.ConnectionSqlServer!);
            return categories;
        }

        public CategoryEntity? GetCategoryById(int id)
        {
            string query = "SELECT * FROM Categories WHERE CategoryID = @CategoryID";
            IEnumerable<CategoryEntity> categories = _helpersQuery.ExecuteQuery<CategoryEntity, object>(query,
                new { @CategoryID = id },
                _connectionStrings.ConnectionSqlServer!);
            return categories.FirstOrDefault();
        }
    }
}
