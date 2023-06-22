using Core.Dto.InputsOutputs;

namespace Infrastructure.Interface.Repository.InputsOutputs
{
    public interface IInputsOutputsCommandRepository
    {
        ResponseDB CreateInputsOutputs(InputsOutputsCreateDto inputsOutputsCrea);
    }
}
