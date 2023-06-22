namespace Core.Entity
{
    public class LoginEntity
    {
        public string? AuthToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public LoginInfo? LoginInfo { get; set; }
    }

    public class LoginInfo
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public long Phone { get; set; }
        public int RoleId { get; set; }
        public bool StatusUser { get; set; }
    }
}
