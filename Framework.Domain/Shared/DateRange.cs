using System;

namespace Framework.Domain.Shared
{
    public class DateRange : ValueObject<DateRange>
    {
        public DateTime From { get; private set; }
        public DateTime Thru { get; private set; }

        public DateRange()
        {
        }

        public DateRange(DateTime from, DateTime thru)
        {
            if (from > thru)
            {
                throw new ArgumentOutOfRangeException("Thru");
            }

            From = from;
            Thru = thru;
        }

        public bool HasOverlaps(DateRange other)
        {
            if ((other.From <= From && From <= other.Thru) ||
                (From <= other.From && other.From <= Thru))
            {
                return true;
            }

            return false;
        }

        public override bool Equals(DateRange other)
        {
            return From == other.From && Thru == other.Thru;
        }
    }
}
