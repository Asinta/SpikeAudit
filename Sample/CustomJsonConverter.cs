using System;
using System.Linq;
using System.Reflection;
using Audit.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AuditSpike
{
    public class CustomJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        public CustomJsonConverter(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);
            var jo = new JObject();
            
            if (t.Type != JTokenType.Object)
            {
                // t.WriteTo(writer);
                jo.WriteTo(writer);
            }
            else
            {
                if (value.GetType() == typeof(AuditEvent))
                {
                    var type = (value as AuditEvent).Target.Old.GetType();
                    foreach (PropertyInfo propInfo in type.GetProperties())
                    {
                        if (propInfo.CanRead)
                        {
                            object propVal = propInfo.GetValue(value, null);
                        
                            Console.WriteLine($"propInfo: {propInfo}");

                            var cutomAttribute = propInfo.GetCustomAttribute<CustomAttribute>();
                            if (cutomAttribute == null)
                            {
                                jo.Add(propInfo.Name, JToken.FromObject(new { propVal }, serializer));
                            }
                        }
                    }
                    
                    jo.WriteTo(writer);
                }
                
                
                //
                //
                //
                // IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
                //
                // o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));
                //
                // o.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException(
                "Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}