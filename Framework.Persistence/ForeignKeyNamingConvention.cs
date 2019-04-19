using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Framework.Persistence
{
    public class RemoveUnderscoreForeignKeyNamingConvention : IStoreModelConvention<AssociationType>
    {
        public void Apply(AssociationType item, DbModel model)
        {
            if (!item.IsForeignKey)
            {
                return;
            }

            ReferentialConstraint constraint = item.Constraint;
            ICollection<EdmProperty> fromProperties = constraint.FromProperties;
            ICollection<EdmProperty> toProperties = constraint.ToProperties;
            if (DoPropertiesHaveDefaultNames(fromProperties, toProperties))
            {
                NormalizeForeignKeyProperties(fromProperties);
            }

            if (DoPropertiesHaveDefaultNames(toProperties, fromProperties))
            {
                NormalizeForeignKeyProperties(toProperties);
            }
        }

        private static bool DoPropertiesHaveDefaultNames(ICollection<EdmProperty> properties,
                                                         ICollection<EdmProperty> otherEndProperties)
        {
            if (properties.Count != otherEndProperties.Count)
            {
                return false;
            }

            using (IEnumerator<EdmProperty> propertiesEnumerator = properties.GetEnumerator())
            using (IEnumerator<EdmProperty> otherEndPropertiesEnumerator = otherEndProperties.GetEnumerator())
            {
                while (propertiesEnumerator.MoveNext() && otherEndPropertiesEnumerator.MoveNext())
                {
                    if (!propertiesEnumerator.Current.Name.EndsWith("_" + otherEndPropertiesEnumerator.Current.Name))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void NormalizeForeignKeyProperties(IEnumerable<EdmProperty> properties)
        {
            foreach (EdmProperty edmProperty in properties)
            {
                int underscoreIndex = edmProperty.Name.IndexOf('_');
                if (underscoreIndex > 0)
                {
                    edmProperty.Name = edmProperty.Name.Remove(underscoreIndex, 1);
                }
            }
        }
    }
}

