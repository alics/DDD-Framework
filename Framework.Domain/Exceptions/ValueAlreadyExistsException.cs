
namespace Framework.Domain.Exceptions
{
    public class ValueAlreadyExistsException : DomainException
    {
        public ValueAlreadyExistsException(string propertyName) :
            base(string.Format(ExceptionMessages.InvalidPropertyException, propertyName))
        {
        }
    }
}
