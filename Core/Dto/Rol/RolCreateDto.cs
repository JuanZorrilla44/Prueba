namespace Core.Dto.Rol
{
    public class RolCreateDto
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string? NameRol { get; set; }
    }
}
