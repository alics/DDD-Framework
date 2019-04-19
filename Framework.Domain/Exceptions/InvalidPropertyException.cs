
namespace Framework.Domain.Exceptions
{
    public class InvalidPropertyException : DomainException
    {
        public InvalidPropertyException(string propertyName) :
            base(string.Format(ExceptionMessages.InvalidPropertyException, propertyName))
        {
        }
    }
}