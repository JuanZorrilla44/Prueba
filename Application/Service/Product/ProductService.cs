using Core.Dto.Product;
using Core.Interface.Services.Product;
using Infrastructure.Interface.Repository.InputsOutputs;
using Infrastructure.Interface.Repository.Product;

namespace Application.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductCommandRepository _productCommandRepository;
        private readonly IInputsOutputsCommandRepository _inputsOutputsCommandRepository;

        public ProductService(IProductQueryRepository productQueryRepository, IProductCommandRepository productCommandRepository, IInputsOutputsCommandRepository i_nputsOutputsCommandRepository)
        {
            _productQueryRepository = productQueryRepository;
            _productCommandRepository = productCommandRepository;
            this._inputsOutputsCommandRepository = i_nputsOutputsCommandRepository;
        }

        public ResponseService<string> ChangeStatusProduct(int productId)
        {
            if (productId <= 0)
            {
                return new ResponseService<string>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var product = _productQueryRepository.GetProductById(productId);

                if (product == null)
                {
                    return new ResponseService<string>
                    {
                        Error = "El producto no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                var response = _productCommandRepository.ChangeStatusProduc(productId, product.StatusProduct ? 0 : 1);

                if (response.Success)
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.Succes,
                        Success = true,
                        Value = response.Status
                    };
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.DB,
                        Success = false,
                        Error = response.Status
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

        public ResponseService<string> CreateProduct(ProductCreateDto productCreate)
        {
            if (string.IsNullOrEmpty(productCreate.NameProduct) || productCreate.UserId <= 0
                || productCreate.Quantity <= 0 || productCreate.Price <= 0
                || productCreate.CategoryId <= 0)
            {
                return new ResponseService<string>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _productCommandRepository.CreateProduct(productCreate);
                if (response.Success)
                {
                    var responseinputsOutputs = _inputsOutputsCommandRepository.CreateInputsOutputs(new Core.Dto.InputsOutputs.InputsOutputsCreateDto()
                    { 
                        ProductId = int.Parse(response.Status!), 
                        Quantity = productCreate.Quantity, 
                        UserId = productCreate.UserId,
                        TypeInputsOutputs = "+"
                    });

                    if (responseinputsOutputs.Success)
                    {
                        return new ResponseService<string>
                        {
                            Status = EStatusErrors.Succes,
                            Success = true,
                            Value = responseinputsOutputs.Status
                        };
                    }
                    else
                    {
                        return new ResponseService<string>
                        {
                            Status = EStatusErrors.DB,
                            Success = false,
                            Error = responseinputsOutputs.Status
                        };
                    }
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.DB,
                        Success = false,
                        Error = response.Status
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

        public ResponseService<IEnumerable<ProductEntity>> GetAllProductsByCategoryId(int categoryId)
        {
            if (categoryId <= 0)
            {
                return new ResponseService<IEnumerable<ProductEntity>>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _productQueryRepository.GetAllProductsByCategoryId(categoryId);

                return new ResponseService<IEnumerable<ProductEntity>>
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = response
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<ProductEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<IEnumerable<ProductEntity>> GetAllProductsByStatus(int status)
        {
            if (status < 0 || status >= 2)
            {
                return new ResponseService<IEnumerable<ProductEntity>>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _productQueryRepository.GetAllProductsByStatus(status);

                return new ResponseService<IEnumerable<ProductEntity>>
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = response
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<ProductEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<ProductEntity> GetProductById(int productId)
        {
            if (productId <= 0)
            {
                return new ResponseService<ProductEntity>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var product = _productQueryRepository.GetProductById(productId);

                if (product == null)
                {
                    return new ResponseService<ProductEntity>
                    {
                        Error = "El producto no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                return  new ResponseService<ProductEntity>
                {
                    Status = EStatusErrors.Succes,
                    Success = true,
                    Value = product
                };

            }
            catch (Exception ex)
            {
                return new ResponseService<ProductEntity>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<string> UpdateProduct(ProductUpdateDto productUpdate)
        {
            if (string.IsNullOrEmpty(productUpdate.NameProduct) || productUpdate.UserId <= 0
               || productUpdate.Quantity <= 0 || productUpdate.Price <= 0
               || productUpdate.CategoryId <= 0 || productUpdate.ProductId <= 0
               || string.IsNullOrEmpty(productUpdate.TypeValue))
            {
                return new ResponseService<string>
                {
                    Error = "Ese tipo de estatus no esta permitido",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var product = _productQueryRepository.GetProductById(productUpdate.ProductId); 
                var quantityOriginal = productUpdate.Quantity;

                if (product == null)
                {
                    return new ResponseService<string>
                    {
                        Error = "El producto no existe",
                        Status = EStatusErrors.DB,
                        Success = false,
                    };
                }

                /*Validate TypeValue sum o menos */

                switch (productUpdate.TypeValue)
                {
                    case "-":
                        productUpdate.Quantity = product.Quantity - productUpdate.Quantity;
                        break;
                    case "+":
                        productUpdate.Quantity = product.Quantity + productUpdate.Quantity;
                        break;
                    default:
                        return new ResponseService<string>
                        {
                            Error = "No se permite ningun otro caracter",
                            Status = EStatusErrors.Data,
                            Success = false,
                        };
                }

                var response = _inputsOutputsCommandRepository.CreateInputsOutputs(new Core.Dto.InputsOutputs.InputsOutputsCreateDto()
                {
                    ProductId = productUpdate.ProductId,
                    Quantity = quantityOriginal,
                    UserId = productUpdate.UserId,
                    TypeInputsOutputs = productUpdate.TypeValue
                });

                if (response.Success)
                {
                    var responseUpdate = _productCommandRepository.UpdateProduct(productUpdate);

                    if (responseUpdate.Success)
                    {
                        return new ResponseService<string>
                        {
                            Status = EStatusErrors.Succes,
                            Success = true,
                            Value = responseUpdate.Status
                        };
                    }
                    else
                    {
                        return new ResponseService<string>
                        {
                            Status = EStatusErrors.DB,
                            Success = false,
                            Error = responseUpdate.Status
                        };
                    }
                }
                else
                {
                    return new ResponseService<string>
                    {
                        Status = EStatusErrors.DB,
                        Success = false,
                        Error = response.Status
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
