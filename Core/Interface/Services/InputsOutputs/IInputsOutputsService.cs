namespace Core.Interface.Services.InputsOutputs
{
    public interface IInputsOutputsService
    {
        ResponseService<IEnumerable<InputsOutputsEntity>> GetAllInputsOutputsByProductId(int productId);
        ResponseService<IEnumerable<InputsOutputsEntity>> GetAllInputsOutputsByUserId(int userId);
        ResponseService<IEnumerable<InputsOutputsEntity>> GetAllInputsOutputs();
    }
}
