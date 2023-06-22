namespace Core.Dto.Category
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string? NameCategory { get; set; }
    }
}
