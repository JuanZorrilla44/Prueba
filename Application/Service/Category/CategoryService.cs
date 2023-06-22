using Core.Dto.Category;
using Core.Interface.Services.Category;
using Infrastructure.Interface.Repository.Category;
using Infrastructure.Interface.Repository.Product;

namespace Application.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly ICategoryCommandRepository _commandRepository;
        private readonly IProductQueryRepository _productQueryRepository;

        public CategoryService(ICategoryQueryRepository categoryQueryRepository, ICategoryCommandRepository commandRepository, IProductQueryRepository productQueryRepository)
        {
            _categoryQueryRepository = categoryQueryRepository;
            _commandRepository = commandRepository;
            _productQueryRepository = productQueryRepository;
        }

        public ResponseService<string> ChangeStatusCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return new ResponseService<string>()
                {
                    Error = "Los datos no puede llegar nulos o negativos",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                CategoryEntity? categoryEntity = _categoryQueryRepository.GetCategoryById(categoryId);

                if (categoryEntity == null)
                {
                    return new ResponseService<string>()
                    {
                        Error = "La categoria no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var response = _commandRepository.ChangeCategoryStatus(categoryId, categoryEntity.StatusCategory ? 0 : 1);

                if(response.Success)
                {
                    return new ResponseService<string>()
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = response.Status
                    };
                }
                else
                {
                    return new ResponseService<string>()
                    {
                        Error = response.Status,
                        Success = false,
                        Status = EStatusErrors.DB,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<string> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            if (string.IsNullOrEmpty(categoryCreateDto.NameCategory))
            {
                return new ResponseService<string>()
                {
                    Error = "Los datos no puede llegar nulos o negativos",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _commandRepository.CreateCategory(categoryCreateDto);

                if (response.Success)
                {
                    return new ResponseService<string>()
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = response.Status
                    };
                }
                else
                {
                    return new ResponseService<string>()
                    {
                        Error = response.Status,
                        Success = false,
                        Status = EStatusErrors.DB,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<string> DeleteCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return new ResponseService<string>()
                {
                    Error = "Los datos no puede llegar nulos o negativos",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                CategoryEntity? categoryEntity = _categoryQueryRepository.GetCategoryById(categoryId);

                if (categoryEntity == null)
                {
                    return new ResponseService<string>()
                    {
                        Error = "La categoria no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var validateProductByCategory = _productQueryRepository.GetAllProductsByCategoryId(categoryId);

                if(validateProductByCategory.Any())
                {
                    return new ResponseService<string>()
                    {
                        Error = "No se permie eliminar la categoria porque tiene productos asociados",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var response = _commandRepository.DeleteCategory(categoryId);

                if (response.Success)
                {
                    return new ResponseService<string>()
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = response.Status
                    };
                }
                else
                {
                    return new ResponseService<string>()
                    {
                        Error = response.Status,
                        Success = false,
                        Status = EStatusErrors.DB,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<IEnumerable<CategoryEntity>> GetAllCategoriesByStatus(int status)
        {
            if (status < 0 || status >= 2)
            {
                return new ResponseService<IEnumerable<CategoryEntity>>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _categoryQueryRepository.GetAllCategoriesByStatus(status);

                return new ResponseService<IEnumerable<CategoryEntity>>()
                {
                    Value = response,
                    Status = EStatusErrors.Succes,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<CategoryEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<CategoryEntity> GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                return new ResponseService<CategoryEntity>()
                {
                    Error = "Los datos no puede llegar nulos o negativos",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                CategoryEntity? categoryEntity = _categoryQueryRepository.GetCategoryById(categoryId);

                if (categoryEntity == null)
                {
                    return new ResponseService<CategoryEntity>()
                    {
                        Error = "La categoria no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                return new ResponseService<CategoryEntity>()
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = categoryEntity
                };

            }
            catch (Exception ex)
            {
                return new ResponseService<CategoryEntity>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<string> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            if (string.IsNullOrEmpty(categoryUpdateDto.NameCategory) || categoryUpdateDto.CategoryID <= 0)
            {
                return new ResponseService<string>()
                {
                    Error = "Los datos no puede llegar nulos o negativos",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _commandRepository.UpdateCategory(categoryUpdateDto);

                if (response.Success)
                {
                    return new ResponseService<string>()
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = response.Status
                    };
                }
                else
                {
                    return new ResponseService<string>()
                    {
                        Error = response.Status,
                        Success = false,
                        Status = EStatusErrors.DB,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<string>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }
    }
}
