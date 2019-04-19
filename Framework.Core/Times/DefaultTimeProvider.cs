using System;

namespace Framework.Core.Times
{
    public class DefaultTimeProvider : ITimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
