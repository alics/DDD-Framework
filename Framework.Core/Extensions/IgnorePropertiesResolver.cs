using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Core.Extensions
{
    public class IgnorePropertiesResolver : DefaultContractResolver
	{
		private readonly HashSet<string> ignoreProps;
		public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
		{
			if (propNamesToIgnore != null && propNamesToIgnore.Any())
			{
				this.ignoreProps = new HashSet<string>(propNamesToIgnore.Select(n => n.ToLower()));
			}
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty property = base.CreateProperty(member, memberSerialization);

			try
			{
				if (ignoreProps != null && ignoreProps.Any())
				{
					if (ignoreProps.Contains(property.PropertyName.ToLower()))
					{
						property.ShouldSerialize = _ => false;
					}
				}
			}
			catch(Exception ex)
            {

            }

			return property;
		}
	}
}