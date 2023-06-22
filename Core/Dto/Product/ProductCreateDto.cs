namespace Core.Dto.Product
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string? NameProduct { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int UserId { get; set; }
    }
}
