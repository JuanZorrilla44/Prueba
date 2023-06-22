namespace Core.Dto.Rol
{
    public class RolUpdateDto
    {

        [Required(ErrorMessage = "Este campo es requerido")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string? NameRol { get; set; }
    }
}
