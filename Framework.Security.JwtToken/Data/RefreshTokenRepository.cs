using System;
using System.Linq;

namespace Framework.Security.JwtToken.Data
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public RefreshToken Get(string tokenText)
        {
            using (var context = new SecurityTokenContext())
            {
                return context.Set<RefreshToken>().FirstOrDefault(n => n.Text == tokenText.ToString());
            }
        }

        public void Create(RefreshToken refreshToken)
        {
            using (var context = new SecurityTokenContext())
            {
                context.Set<RefreshToken>().Add(refreshToken);
                context.SaveChanges();
            }
        }

        public void Delete(string tokenText)
        {
            using (var context = new SecurityTokenContext())
            {
                var refreshToken = context.Set<RefreshToken>().FirstOrDefault(n => n.Text == tokenText.ToString());
                context.Set<RefreshToken>().Remove(refreshToken);
                context.SaveChanges();
            }
        }

        public void ExpireOldTokensByUserIdentity(string userIdentity)
        {
            using (var context = new SecurityTokenContext())
            {
                var refreshTokens = context.Set<RefreshToken>().Where(n => n.UserIdentity == userIdentity);
                foreach (var refreshToken in refreshTokens)
                {
                    refreshToken.ExpireDate = DateTime.Now.AddMinutes(-1);
                }
                
                context.SaveChanges();
            }
        }
    }
}