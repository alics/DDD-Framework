using System;

namespace Framework.Core.ExceptionHandling
{
    public class InvalidMaxLengthArgumentException : BusinessException<CrmGeneralErrorType>
    {
        public InvalidMaxLengthArgumentException(string propertyName, Exception innerException, object data = null, bool returnDetail = true) :
            base(CrmGeneralErrorType.InvalidMaxLengthArgumentException, propertyName, data, returnDetail)
        {
        }
    }
}