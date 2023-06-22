namespace Core.Dto.User
{
    public record UserUpdateDto
    {
        public UserUpdateDto(
            int userId,
            string email,
            string fullName,
            string userName,
            string phone)
        {
            UserId = userId;
            Email = email;
            FullName = fullName;
            UserName = userName;
            Phone = phone;
        }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "El nombre de usuario debe se entre 8 y 15 digitos.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El campo debe tener exactamente 10 dígitos.")]
        public string Phone { get; set; }
    }
}
