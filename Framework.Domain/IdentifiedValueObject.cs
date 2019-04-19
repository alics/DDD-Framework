namespace Framework.Domain
{
    public abstract class IdentifiedValueObject<TKey> : ValueObject<IdentifiedValueObject<TKey>>         
    {
        public abstract TKey GetIdentity();
        public override bool Equals(IdentifiedValueObject<TKey> other)
        {
            if (other == null)
            {
                return false;
            }
            return this.GetIdentity().Equals(other.GetIdentity());
        }
    }
}
