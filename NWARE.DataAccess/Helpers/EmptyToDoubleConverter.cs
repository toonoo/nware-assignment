using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NWARE.Business.Helpers
{
    public class EmptyToDoubleConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                if (string.IsNullOrEmpty(str)) return 0;
                if (double.TryParse(str, out double value)) return value;
                return 0;
            }

            if (reader.TokenType == JsonTokenType.Null) return 0;

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                reader.Skip();
                return 0;
            }

            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
