using System;

namespace Framework.Core.ExceptionHandling
{
    public interface IBusinessException
    {
        object GetData();
        int GetCode();
        string GetMessage();
        bool ReturnDetail();
    }

	public interface IBusinessException<TErrorType> : IBusinessException 
		where TErrorType : Enum
	{
		TErrorType Code { get; }
	}
}
