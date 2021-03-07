
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace GameSystemObjects
{
    public static class Formatter
    {
        
            public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

        public static MediaTypeFormatter[] MediaTypeFormatters { get; } = new MediaTypeFormatter[]
            {
            new JsonMediaTypeFormatter
            {
                SerializerSettings = SerializerSettings,
            },
            };

            public static MediaTypeFormatter MediaTypeFormatter { get; } = new JsonMediaTypeFormatter
            {
                SerializerSettings = SerializerSettings,
            };
        
    }
}
