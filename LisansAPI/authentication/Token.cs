namespace LisansAPI.authentication
{
    internal class JWTTokenResponse

    {   public SystemUser User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}