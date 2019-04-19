using System;

namespace Framework.Domain.Exceptions
{
    public class MinValueException<T> : DomainException
        where T : struct
    {
        public MinValueException(string propertyName, T minValue) : 
            base(String.Format(ExceptionMessages.MinValueException, propertyName, minValue))
        {
        }
    }
}
