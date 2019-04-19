using System;

namespace Framework.Domain
{
    public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
    {
        public abstract bool Equals(TValueObject other);

        public override bool Equals(object obj)
        {
            return Equals((TValueObject)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
