using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.Workflow
{
    public interface IAssigneeDetector
    {
        string GetAssignee(params string[] parameters);
    }
}
