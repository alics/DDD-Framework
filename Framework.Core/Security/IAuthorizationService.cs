using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Core.Security
{
    public interface IAuthorizationService
    {
        Task<bool> IsAuthorized(string securityPrincipalCode, string operationCode);
        Task<IEnumerable<string>> GetOperationCodeOfPermissions(string securityPrincipalCode);
        Task<IEnumerable<string>> GetOperationCodeOfPermissions(string securityPrincipalCode, string tag);
        long GetSecurityPrincipleIdOfCurrentUser(string securityPrincipalCode);
    }
}
