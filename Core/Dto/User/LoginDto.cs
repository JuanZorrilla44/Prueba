namespace Core.Dto.User
{
    public record LoginDto
    {
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Password { get; set; }
    }
}
