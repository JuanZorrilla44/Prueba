using Core.Entity;

namespace Infrastructure.Interface.Repository.InputsOutputs
{
    public interface IInputsOutputsQueryRepository
    {
        IEnumerable<InputsOutputsEntity> GetAllInputsOutputsByProductId(int productId);
        IEnumerable<InputsOutputsEntity> GetAllInputsOutputsByUserId(int userId);
        IEnumerable<InputsOutputsEntity> GetAllInputsOutputs();
    }
}
