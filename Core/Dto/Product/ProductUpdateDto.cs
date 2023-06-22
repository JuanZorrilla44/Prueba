namespace Core.Dto.Product
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int ProductId { get; set; }
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
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Solo se permite un digito")]
        public string? TypeValue { get; set; }
    }
}
