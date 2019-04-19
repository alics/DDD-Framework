using System;
using System.Collections.Generic;

namespace Framework.Domain
{
    [Serializable]
    public abstract class Entity<TKey, TEntity> : IEquatable<TEntity>
        where TEntity : Entity<TKey, TEntity>
    {
        public TKey Id { get; protected set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public bool Equals(TEntity other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TKey, TEntity>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (IsTransient())
            {
                return false;
            }

            return Id.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default(TKey));
        }
    }
}
