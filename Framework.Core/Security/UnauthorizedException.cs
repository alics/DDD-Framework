using Framework.Core.ExceptionHandling;

namespace Framework.Core.Security
{
    public class UnauthorizedException : BusinessException<CrmGeneralErrorType>
    {
        public UnauthorizedException( object data = null, bool returnDetail = true) :
            base(CrmGeneralErrorType.UnauthorizedException, "UnauthorizedException", data, returnDetail)
        {
        }
    }
}
