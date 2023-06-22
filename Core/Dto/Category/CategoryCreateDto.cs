namespace Core.Dto.Category
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string? NameCategory { get; set; }
    }
}
