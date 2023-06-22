using Core.Dto.InputsOutputs;
using Infrastructure.Interface.Repository.InputsOutputs;

namespace Infrastructure.Repository.InputsOutputs
{
    public class InputsOutputsCommandRepository : IInputsOutputsCommandRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly IHelpersCommand _helpersCommand;

        public InputsOutputsCommandRepository(IOptions<ConnectionStrings> connectionStrings, IHelpersCommand helpersCommand)
        {
            _connectionStrings = connectionStrings.Value;
            _helpersCommand = helpersCommand;
        }

        public ResponseDB CreateInputsOutputs(InputsOutputsCreateDto inputsOutputsCrea)
        {
            string commandInsert = "INSERT INTO InputsOutputs (ProductId, UserId, Quantity,TypeInputsOutputs ) VALUES (@ProductId,@UserId,@Quantity,@TypeInputsOutputs); SELECT CAST(SCOPE_IDENTITY() as int)";
            int execute = _helpersCommand.ExecuteCommad(
               commandInsert,
               inputsOutputsCrea,
               _connectionStrings.ConnectionSqlServer!,
               ETypeCommand.Insert.GetStringValue());
            return new ResponseDB()
            {
                Status = execute > 0 ? "Exito al insertar la informacion" : "Error al  insertar la informacion",
                Success = execute > 0,
            };
        }
    }
}
