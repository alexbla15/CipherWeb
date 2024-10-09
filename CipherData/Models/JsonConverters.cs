using CipherData.Models.Randomizers;
using CipherData.Randomizer;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CipherData.Models
{
    public class JsonIGroupedBooleanConditionConverter : JsonConverter<IGroupedBooleanCondition>
    {
        public override IGroupedBooleanCondition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<GroupedBooleanCondition>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IGroupedBooleanCondition value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (GroupedBooleanCondition)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as random object
                JsonSerializer.Serialize(writer, (RandomGroupedBooleanCondition)value, options);
            }
        }
    }

    public class JsonICategoryConverter : JsonConverter<ICategory>
    {
        public override ICategory Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<Category>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, ICategory value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (Category)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomCategory)value, options);
            }
        }
    }

    public class JsonICategoryPropertyConverter : JsonConverter<ICategoryProperty>
    {
        public override ICategoryProperty Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<CategoryProperty>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, ICategoryProperty value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (CategoryProperty)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomCategoryProperty)value, options);
            }
        }
    }

    /// <summary>
    /// Custom DateTime converter
    /// </summary>
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateTimeFormat = "yyyy-MM-dd HH:mm"; // Format excluding seconds

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _dateTimeFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateTimeFormat));
        }
    }

    /// <summary>
    /// Custom DateTime converter
    /// </summary>
    public class JsonConditionConverter : JsonConverter<Condition>
    {
        public override Condition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse the JSON object without consuming the reader's input
            JsonDocument doc = JsonDocument.ParseValue(ref reader);

            // Get the root element of the parsed JSON object
            JsonElement root = doc.RootElement;

            // Infer type by checking for specific properties
            if (root.TryGetProperty("Attribute", out _))
            {
                // This is likely a BooleanCondition
                return JsonSerializer.Deserialize<BooleanCondition>(root.GetRawText(), options);
            }
            else if (root.TryGetProperty("Conditions", out _))
            {
                // This is likely a GroupedBooleanCondition
                return JsonSerializer.Deserialize<GroupedBooleanCondition>(root.GetRawText(), options);
            }
            else
            {
                throw new JsonException("Unknown condition type based on JSON content.");
            }
        }

        public override void Write(Utf8JsonWriter writer, Condition value, JsonSerializerOptions options)
        {
            // Serialize the resource, including the type
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
