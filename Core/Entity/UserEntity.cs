namespace Core.Entity
{
    public class UserEntity
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
