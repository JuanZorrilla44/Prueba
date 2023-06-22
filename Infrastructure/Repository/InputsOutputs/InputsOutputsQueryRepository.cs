using Core.Entity;
using Infrastructure.Interface.Repository.InputsOutputs;

namespace Infrastructure.Repository.InputsOutputs
{
    public class InputsOutputsQueryRepository : IInputsOutputsQueryRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersQuery _helpersQuery;

        public InputsOutputsQueryRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersQuery helpersQuery)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersQuery = helpersQuery;
        }
        public IEnumerable<InputsOutputsEntity> GetAllInputsOutputs()
        {
            string query = """
                            SELECT I.CreationDate, I.InputsOutputsId, I.ProductId, I.Quantity, I.UserId, U.UserName, P.NameProduct
                            FROM InputsOutputs I
                            INNER JOIN Products P ON I.ProductId = P.ProductId
                            INNER JOIN Users U ON I.UserId = U.UserId
                            """;
            IEnumerable<InputsOutputsEntity> inputsOutputsEntity = _helpersQuery.ExecuteQuery<InputsOutputsEntity>(query, _connectionStrings.ConnectionSqlServer!);
            return inputsOutputsEntity;
        }

        public IEnumerable<InputsOutputsEntity> GetAllInputsOutputsByProductId(int productId)
        {
            string query = """
                            SELECT I.CreationDate, I.InputsOutputsId, I.ProductId, I.Quantity, I.UserId, U.UserName, P.NameProduct
                            FROM InputsOutputs I
                            INNER JOIN Products P ON I.ProductId = P.ProductId
                            INNER JOIN Users U ON I.UserId = U.UserId
                            WHERE I.ProductId = @ProductId
                            """;
            IEnumerable<InputsOutputsEntity> inputsOutputsEntity = _helpersQuery.ExecuteQuery<InputsOutputsEntity, object>(query,
                new { @ProductId = productId },
                _connectionStrings.ConnectionSqlServer!);
            return inputsOutputsEntity;
        }

        public IEnumerable<InputsOutputsEntity> GetAllInputsOutputsByUserId(int userId)
        {
            string query = """
                            SELECT I.CreationDate, I.InputsOutputsId, I.ProductId, I.Quantity, I.UserId, U.UserName, P.NameProduct
                            FROM InputsOutputs I
                            INNER JOIN Products P ON I.ProductId = P.ProductId
                            INNER JOIN Users U ON I.UserId = U.UserId
                            WHERE I.UserId = @UserId
                            """;
            IEnumerable<InputsOutputsEntity> inputsOutputsEntity = _helpersQuery.ExecuteQuery<InputsOutputsEntity, object>(query,
                new { @UserId = userId },
                _connectionStrings.ConnectionSqlServer!);
            return inputsOutputsEntity;
        }
    }
}
