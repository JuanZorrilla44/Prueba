using Core.Dto.Category;
using Infrastructure.Interface.Repository.Category;

namespace Infrastructure.Repository.Category
{
    public class CategoryCommandRepository : ICategoryCommandRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersCommand _helpersCommand;

        public CategoryCommandRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersCommand helpersCommand)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersCommand = helpersCommand;
        }

        public ResponseDB ChangeCategoryStatus(int categoryId, int status)
        {
            string commandUpdate = "UPDATE Categories SET StatusCategory = @Status WHERE CategoryID = @CategoryID";
            int execute = _helpersCommand.ExecuteCommad<object>(
                commandUpdate,
                new { @Status = status, @CategoryID = categoryId },
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al cambiar el estado" : "Error al cambiar el estado",
                Success = execute > 0,
            };
        }

        public ResponseDB CreateCategory(CategoryCreateDto categoryCreate)
        {
            string commandCreate = "INSERT INTO Categories (NameCategory) VALUES (@NameCategory); SELECT CAST(SCOPE_IDENTITY() as int)";
            int execute = _helpersCommand.ExecuteCommad(
                commandCreate,
                categoryCreate,
                _connectionStrings.ConnectionSqlServer!,
                ETypeCommand.Insert.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al insertar informacio" : "Error al insertar la informacion",
                Success = execute > 0,
            };
        }

        public ResponseDB DeleteCategory(int categoryId)
        {
            string commandDelete = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
            int execute = _helpersCommand.ExecuteCommad<object>(
               commandDelete,
               new { @CategoryID = categoryId },
               _connectionStrings.ConnectionSqlServer!,
               ETypeCommand.Delete.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al borrar la cateogria" : "Error al eliminar",
                Success = execute > 0,
            };
        }

        public ResponseDB UpdateCategory(CategoryUpdateDto categoryUpdate)
        {
            string commandUpdate = "UPDATE Categories SET NameCategory = @NameCategory WHERE CategoryID = @CategoryID";
            int execute = _helpersCommand.ExecuteCommad(
              commandUpdate,
              categoryUpdate,
              _connectionStrings.ConnectionSqlServer!,
              ETypeCommand.Update.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al cambiar la informacion de la categoria" : "Error al cambiar la informacion",
                Success = execute > 0,
            };
            throw new NotImplementedException();
        }
    }
}
