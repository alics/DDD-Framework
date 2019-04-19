using System.Transactions;

namespace Framework.Core.Transactions
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope CreateScope()
        {
            var transactionOptions = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }
    }
}
