namespace Core.Dto.InputsOutputs
{
    public class InputsOutputsCreateDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? TypeInputsOutputs { get; set; }
    }
}
