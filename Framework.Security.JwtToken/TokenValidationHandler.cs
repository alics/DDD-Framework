using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace Framework.Security.JwtToken
{
    public class TokenValidationHandler : DelegatingHandler
    {
        private TokenValidationParameters tokenValidationParameters;

        public Action tokenValidatedAction;

        public TokenValidationHandler(Action tokenValidatedAction, TokenValidationParameters tokenValidationParameters = null)
        {
            this.tokenValidationParameters = tokenValidationParameters;
            this.tokenValidatedAction = tokenValidatedAction;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                if (tokenValidationParameters == null)
                {
                    tokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = TokenConstants.ValidAudience,
                        ValidIssuer = TokenConstants.ValidIssuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtSecurityKey.Create(TokenConstants.SymmettricKey)
                    };
                }

                var handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                Thread.CurrentPrincipal = handler.ValidateToken(token, tokenValidationParameters, out securityToken);
                HttpContext.Current.User = handler.ValidateToken(token, tokenValidationParameters, out securityToken);
                tokenValidatedAction();

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        private bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }
    }
}
