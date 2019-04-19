using Framework.Core.Exceptions;
using System;

namespace Framework.Domain.Exceptions
{
    public class DomainException : ExceptionBase
    {
        protected DomainException()
        {
        }

        protected DomainException(string message) : base(message)
        {
        }

        protected DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}