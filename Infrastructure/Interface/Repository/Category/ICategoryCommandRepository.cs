using Core.Dto.Category;

namespace Infrastructure.Interface.Repository.Category
{
    public interface ICategoryCommandRepository
    {
        ResponseDB CreateCategory(CategoryCreateDto categoryCreate);
        ResponseDB UpdateCategory(CategoryUpdateDto categoryUpdate);
        ResponseDB ChangeCategoryStatus(int  categoryId, int status);
        ResponseDB DeleteCategory(int categoryId);
    }
}
