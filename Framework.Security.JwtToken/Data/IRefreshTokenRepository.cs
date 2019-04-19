namespace Framework.Security.JwtToken.Data
{
    public interface IRefreshTokenRepository
    {
        RefreshToken Get(string tokenText);

        void Create(RefreshToken refreshToken);

        void Delete(string tokenText);

        void ExpireOldTokensByUserIdentity(string userIdentity);
    }
}
