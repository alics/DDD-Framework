using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Extensions
{
    public static class ObjectExtension
	{
		public static List<KeyValuePair<string, string>> ToKeyValuePairs(this object aObject)
		{
			return aObject
				?.GetType()
				?.GetProperties()
				.Select(property => new KeyValuePair<string, string>
					(
						property?.Name,
						property?.GetValue(aObject)?.ToString())
				).ToList();
		}


		public static string ToJson<T>(this T o, bool beautify = false) where T : new()
		{
			try
			{
				var result = JsonConvert.SerializeObject(o, 
					!beautify ? Formatting.None : Formatting.Indented,
						new JsonSerializerSettings
						{
							PreserveReferencesHandling = PreserveReferencesHandling.None,
							NullValueHandling = NullValueHandling.Ignore,
							ReferenceLoopHandling = ReferenceLoopHandling.Ignore
						});
				return result;
			}
			catch
			{
				return null;
			}
		}


		public static T FromJson<T>(this string str) where T : new()
		{
			try
			{
				var result = JsonConvert.DeserializeObject<T>(str);
				return result;
			}
			catch
			{
				return default(T);
			}
		}
	}
}