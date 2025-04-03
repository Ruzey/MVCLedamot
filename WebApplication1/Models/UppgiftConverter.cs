using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class UppgiftConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                return JsonSerializer.Deserialize<List<string>>(ref reader, options);
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                return new List<string> { reader.GetString() };
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                return new List<string> { JsonSerializer.Serialize(JsonDocument.ParseValue(ref reader).RootElement) };
            }

            throw new JsonException($"Unexpected token type {reader.TokenType} for 'uppgift'");
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
