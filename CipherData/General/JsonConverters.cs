using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CipherData.General
{
    public class JsonIGroupedBooleanConditionConverter : JsonConverter<IGroupedBooleanCondition>
    {
        public override IGroupedBooleanCondition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
        public override ICategory? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

    public class JsonIPackageConverter : JsonConverter<IPackage>
    {
        public override IPackage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<Package>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IPackage value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (Package)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomPackage)value, options);
            }
        }
    }

    public class JsonIPackagePropertyConverter : JsonConverter<IPackageProperty>
    {
        public override IPackageProperty? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<PackageProperty>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IPackageProperty value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (PackageProperty)value, options);
        }
    }

    public class JsonIVesselConverter : JsonConverter<IVessel>
    {
        public override IVessel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<Vessel>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IVessel value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (Vessel)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomVessel)value, options);
            }
        }
    }

    public class JsonIStorageSystemConverter : JsonConverter<IStorageSystem>
    {
        public override IStorageSystem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<StorageSystem>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IStorageSystem value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (StorageSystem)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomStorageSystem)value, options);
            }
        }
    }

    public class JsonIUnitConverter : JsonConverter<IUnit>
    {
        public override IUnit? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<Unit>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IUnit value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (Unit)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomUnit)value, options);
            }
        }
    }

    public class JsonIProcessDefinitionConverter : JsonConverter<IProcessDefinition>
    {
        public override IProcessDefinition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<ProcessDefinition>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IProcessDefinition value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (ProcessDefinition)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomProcessDefinition)value, options);
            }
        }
    }

    public class JsonIProcessStepDefinitionConverter : JsonConverter<IProcessStepDefinition>
    {
        public override IProcessStepDefinition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into the concrete object type
            return JsonSerializer.Deserialize<ProcessStepDefinition>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IProcessStepDefinition value, JsonSerializerOptions options)
        {
            try
            {
                // Attempt to serialize as object
                JsonSerializer.Serialize(writer, (ProcessStepDefinition)value, options);
            }
            catch (Exception)
            {
                // If serialization fails, try to serialize as Random object
                JsonSerializer.Serialize(writer, (RandomProcessStepDefinition)value, options);
            }
        }
    }

    public class JsonICategoryPropertyConverter : JsonConverter<ICategoryProperty>
    {
        public override ICategoryProperty? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            return DateTime.ParseExact(reader.GetString() ?? string.Empty, _dateTimeFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateTimeFormat));
        }
    }

    /// <summary>
    /// Custom DateTime converter
    /// </summary>
    public class JsonIConditionConverter : JsonConverter<ICondition>
    {
        public override ICondition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse the JSON object without consuming the reader's input
            JsonDocument doc = JsonDocument.ParseValue(ref reader);

            // Get the root element of the parsed JSON object
            JsonElement root = doc.RootElement;

            // Infer type by checking for specific properties
            if (root.TryGetProperty("Attribute", out _))
            {
                // This is likely a BooleanCondition
                return JsonSerializer.Deserialize<IBooleanCondition>(root.GetRawText(), options);
            }
            else if (root.TryGetProperty("Conditions", out _))
            {
                // This is likely a GroupedBooleanCondition
                return JsonSerializer.Deserialize<IGroupedBooleanCondition>(root.GetRawText(), options);
            }
            else
            {
                throw new JsonException("Unknown condition type based on JSON content.");
            }
        }

        public override void Write(Utf8JsonWriter writer, ICondition value, JsonSerializerOptions options)
        {
            // Serialize the resource, including the type
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
