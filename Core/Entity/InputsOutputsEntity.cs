namespace Core.Entity
{
    public class InputsOutputsEntity
    {
        public int InputsOutputsId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string? NameProduct { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
