using System.Collections.Generic;

namespace Framework.Core.Expressions
{
    public interface IExperssionEvaluator
    {
        TOutput Evaluate<TOutput>(string exprssion, Dictionary<string, object> parameters= null);
    }
}
