using Core.Interface.Services.InputsOutputs;
using Infrastructure.Interface.Repository.InputsOutputs;

namespace Application.Service.InputsOutputs
{
    public class InputsOutputsService : IInputsOutputsService
    {
        private readonly IInputsOutputsQueryRepository _inputsOutputsQueryRepository;

        public InputsOutputsService(IInputsOutputsQueryRepository inputsOutputsQueryRepository)
        {
            _inputsOutputsQueryRepository = inputsOutputsQueryRepository;
        }

        public ResponseService<IEnumerable<InputsOutputsEntity>> GetAllInputsOutputs()
        {
            try
            {
                var response = _inputsOutputsQueryRepository.GetAllInputsOutputs();

                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Value = response,
                    Status = EStatusErrors.Succes,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<IEnumerable<InputsOutputsEntity>> GetAllInputsOutputsByProductId(int productId)
        {
            if (productId <= 0)
            {
                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Error = "La informacio no puedo llegar nula",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _inputsOutputsQueryRepository.GetAllInputsOutputsByProductId(productId);

                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Value = response,
                    Status = EStatusErrors.Succes,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }

        public ResponseService<IEnumerable<InputsOutputsEntity>> GetAllInputsOutputsByUserId(int userId)
        {
            if (userId <= 0)
            {
                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Error = "La informacio no puedo llegar nula",
                    Status = EStatusErrors.Data,
                    Success = false,
                };
            }

            try
            {
                var response = _inputsOutputsQueryRepository.GetAllInputsOutputsByUserId(userId);

                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Value = response,
                    Status = EStatusErrors.Succes,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseService<IEnumerable<InputsOutputsEntity>>
                {
                    Error = ex.Message,
                    Status = EStatusErrors.Failed,
                    Success = false,
                };
            }
        }
    }
}
