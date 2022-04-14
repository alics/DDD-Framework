using System;

namespace Framework.Core.ExceptionHandling
{
    public class InvalidRequiredArgumentException : BusinessException<CrmGeneralErrorType>
    {
        public InvalidRequiredArgumentException(string propertyName, Exception innerException, object data = null, bool returnDetail = true) :
            base(CrmGeneralErrorType.InvalidRequiredArgumentException, propertyName, innerException, data, returnDetail)
        {
        }
    }
}
