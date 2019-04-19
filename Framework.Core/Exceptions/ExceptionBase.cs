using System;

namespace Framework.Core.Exceptions
{
    public abstract class ExceptionBase : ApplicationException
    {
        protected ExceptionBase()
        {
        }

        protected ExceptionBase(string message) : base(message)
        {
        }

        protected ExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }
    }


}
