using System;
using Framework.Core.Extensions;

namespace Framework.Core.ExceptionHandling
{
    public class BusinessException<TErrorType> : Exception, IBusinessException<TErrorType>
        where TErrorType : Enum
    {
        private readonly object _internalData;
        private readonly bool _returnDetail;

        public TErrorType Code { get; }

        public BusinessException(TErrorType code, string message, object data, bool returnDetail) : base(message)
        {
            _internalData = data;
            _returnDetail = returnDetail;
            Code = code;
        }

        public BusinessException(TErrorType code, string message, Exception innerException, object data, bool returnDetail) : base(message, innerException)
        {
            _internalData = data;
            _returnDetail = returnDetail;
            Code = code;
        }

        public int GetCode()
        {
            return Code.ToInt();
        }

        public bool ReturnDetail()
        {
            return _returnDetail;
        }

        public object GetData()
        {
            return _internalData;
        }

        public string GetMessage()
        {
            return Message;
        }
    }
}