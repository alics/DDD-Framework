using Framework.Core.Expressions;
using System;
using System.Collections.Generic;

namespace Framework.NCalcExperssions
{
    public class NCalcExperssionEvaluator : IExperssionEvaluator
    {
        public TOutput Evaluate<TOutput>(string expression, Dictionary<string, object> parameters = null)
        {
            var clacExpression = new NCalc.Expression(expression, NCalc.EvaluateOptions.IgnoreCase);
            
            if (parameters != null)
            {
                clacExpression.Parameters = parameters;
            }

            var funcResult = clacExpression.ToLambda<TOutput>();
            return funcResult();
        }
    }
}
