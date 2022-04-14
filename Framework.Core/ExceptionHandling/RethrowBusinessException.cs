using System;

namespace Framework.Core.ExceptionHandling
{
    public class RethrowBusinessException : Exception, IBusinessException
    {
        private readonly object _internalData;
        private readonly bool _returnDetail;

        public int Code { get; }

        public RethrowBusinessException(int code, string message, object data, bool returnDetail) : base(message)
        {
            _internalData = data;
            _returnDetail = returnDetail;
            Code = code;
        }

        public RethrowBusinessException(int code, string message, Exception innerException, object data, bool returnDetail) : base(message, innerException)
        {
            _internalData = data;
            _returnDetail = returnDetail;
            Code = code;
        }

        public int GetCode()
        {
            return Code;
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
