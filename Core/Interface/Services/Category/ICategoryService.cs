using Core.Dto.Category;

namespace Core.Interface.Services.Category
{
    public interface ICategoryService
    {
        ResponseService<string> CreateCategory(CategoryCreateDto categoryCreateDto);
        ResponseService<string> UpdateCategory(CategoryUpdateDto categoryUpdateDto);
        ResponseService<string> DeleteCategory(int categoryId);
        ResponseService<string> ChangeStatusCategory(int categoryId);
        ResponseService<CategoryEntity> GetCategoryById(int categoryId);
        ResponseService<IEnumerable<CategoryEntity>> GetAllCategoriesByStatus(int status);
    }
}
