namespace Framework.Core
{
    public class OperationOptions
    {
        public string CurrentUserId { get; set; }
    }

    public interface IOperationOptionsService
    {
        OperationOptions Get();
        void Set(OperationOptions operationOptions);
    }

    public class OperationOptionsService: IOperationOptionsService
    {
        private OperationOptions _operationOptions;

        public OperationOptions Get()
        {
            return _operationOptions;
        }

        public void Set(OperationOptions operationOptions)
        {
            _operationOptions = operationOptions;
        }
    }
}
