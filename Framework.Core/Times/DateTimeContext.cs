namespace Framework.Core.Times
{
    public static class DateTimeContext
    {
        static DateTimeContext()
        {
            Current = new DefaultTimeProvider();
        }

        public static void SetTimeProvider(ITimeProvider timeProvider)
        {
            Current = timeProvider;
        }

        public static ITimeProvider Current { get; set; }
    }
}
