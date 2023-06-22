namespace Core.Entity
{
    public class TokenEntity
    {
        public TokenEntity(string authToken, DateTime expiresIn)
        {
            AuthToken = authToken;
            ExpiresIn = expiresIn;
        }

        public string AuthToken { get; }
        public DateTime ExpiresIn { get; }
    }
}
