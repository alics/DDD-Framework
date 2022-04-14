using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            var otherEntity = obj as Entity<TKey>;
            return Id.Equals(otherEntity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
