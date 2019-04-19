
namespace Framework.Domain.Exceptions
{
    public class NotSupportedDomainOperationException : DomainException
    {
        public NotSupportedDomainOperationException(): base(ExceptionMessages.NotSupportedDomainOperationException)
        {
        }
    }
}