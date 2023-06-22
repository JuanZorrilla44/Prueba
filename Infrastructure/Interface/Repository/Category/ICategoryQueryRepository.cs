using Core.Entity;

namespace Infrastructure.Interface.Repository.Category
{
    public interface ICategoryQueryRepository
    {
        IEnumerable<CategoryEntity> GetAllCategoriesByStatus(int status);
        CategoryEntity? GetCategoryById(int id);
    }
}
