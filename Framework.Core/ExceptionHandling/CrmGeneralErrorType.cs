namespace Framework.Core.ExceptionHandling
{
    public enum CrmGeneralErrorType
    {
        UnknownError = ErrorCodeConstants.CrmGeneralErrorCodeStartIndex + 1,
        InvalidRequiredArgumentException = ErrorCodeConstants.CrmGeneralErrorCodeStartIndex + 2,
        InvalidMaxLengthArgumentException = ErrorCodeConstants.CrmGeneralErrorCodeStartIndex + 3,
        UnauthorizedException = ErrorCodeConstants.CrmGeneralErrorCodeStartIndex + 4,
    }
}