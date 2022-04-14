using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Framework.Core.ApiStandardResults
{
    public class ApiResult<TResult>
    {
        public bool Succeeded { get; set; }
        public TResult Result { get; set; }
        public ApiError Error { get; set; }

        public static ApiResult<TResult> StandardOk(TResult result)
        {
            return new ApiResult<TResult>
            {
                Succeeded = true,
                Result = result,
                Error = null
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new LongConverter(),
                    new NullabeLongConverter()
                },
                ContractResolver = new CamelCasePropertyNamesContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        ProcessDictionaryKeys = false,
                        OverrideSpecifiedNames = false,
                        ProcessExtensionDataNames = false
                    }
                }
            });
        }
    }

    public class ApiResult : ApiResult<object>
    {
        public static ApiResult StandardOk()
        {
            return new ApiResult
            {
                Succeeded = true,
                Result = null,
                Error = null
            };
        }

        public static ApiResult StandardError(ApiError error)
        {
            return new ApiResult
            {
                Succeeded = false,
                Result = null,
                Error = error
            };
        }
    }
}