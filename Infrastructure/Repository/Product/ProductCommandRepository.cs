using Core.Dto.Product;
using Infrastructure.Interface.Repository.Product;

namespace Infrastructure.Repository.Product
{
    public class ProductCommandRepository : IProductCommandRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersCommand _helpersCommand;

        public ProductCommandRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersCommand helpersCommand)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersCommand = helpersCommand;
        }

        public ResponseDB ChangeStatusProduc(int productId, int status)
        {
            string commandUpdate = "UPDATE Products SET StatusProduct = @Status WHERE CategoryID = @ProductId";
            int execute = _helpersCommand.ExecuteCommad<object>(
                commandUpdate,
                new { @Status = status, @ProductId = productId },
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al cambiar el estado" : "Error al cambiar el estado",
                Success = execute > 0,
            };
        }

        public ResponseDB CreateProduct(ProductCreateDto productCreate)
        {
            string commandInsert = "INSERT INTO Products (NameProduct, CategoryId, Price, Quantity ) VALUES (@NameProduct,@CategoryId,@Price,@Quantity); SELECT CAST(SCOPE_IDENTITY() as int)";
            int execute = _helpersCommand.ExecuteCommad(
               commandInsert,
               productCreate,
               _connectionStrings.ConnectionSqlServer!,
               ETypeCommand.Insert.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? execute.ToString() : "Error al insertar la informacion",
                Success = execute > 0,
            };
        }

        public ResponseDB UpdateProduct(ProductUpdateDto productUpdate)
        {
            string commandUpdate = "UPDATE Products SET NameProduct = @NameProduct, CategoryId = @CategoryId ,Price = @Price, Quantity = @Quantity  WHERE ProductId = @ProductId;";
            int execute = _helpersCommand.ExecuteCommad(
               commandUpdate,
               productUpdate,
               _connectionStrings.ConnectionSqlServer!,
               ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al insertar informacio" : "Error al insertar la informacion",
                Success = execute > 0,
            };
        }
    }
}
