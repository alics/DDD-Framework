using System;

namespace Framework.Core.Times
{
    public interface ITimeProvider
    {
        DateTime GetCurrentTime();
    }
}
