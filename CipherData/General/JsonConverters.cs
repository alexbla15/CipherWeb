using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CipherData.General
{
    public class JsonInterfaceConverter<TInterface, TApiClass, TRandomClass> : JsonConverter<TInterface>
    where TApiClass : TInterface
    where TRandomClass : TInterface
    {
        public override TInterface? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => JsonSerializer.Deserialize<TApiClass>(ref reader, options);

        public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options)
        {
            if (value is TApiClass api_value) JsonSerializer.Serialize(writer, api_value, options);
            if (value is TRandomClass random_value) JsonSerializer.Serialize(writer, random_value, options);
        }
    }

    public class JsonIAggregateItemConverter : 
        JsonInterfaceConverter<IAggregateItem, AggregateItem, AggregateItem> { }
    
    public class JsonICategoryConverter : JsonInterfaceConverter<ICategory, Category, RandomCategory> { }

    public class JsonICategoryPropertyConverter : 
        JsonInterfaceConverter<ICategoryProperty, CategoryProperty, RandomCategoryProperty> { }

    public class JsonIGroupedBooleanConditionConverter : 
        JsonInterfaceConverter<IGroupedBooleanCondition, GroupedBooleanCondition, RandomGroupedBooleanCondition> { }

    public class JsonIObjectFactoryConverter :
        JsonInterfaceConverter<IObjectFactory, ObjectFactory, ObjectFactory>
    { }

    public class JsonIPackageConverter : JsonInterfaceConverter<IPackage, Package, RandomPackage> { }

    public class JsonIPackagePropertyConverter : 
        JsonInterfaceConverter<IPackageProperty, PackageProperty, PackageProperty> { }

    public class JsonIVesselConverter : JsonInterfaceConverter<IVessel, Vessel, RandomVessel> { }

    public class JsonIStorageSystemConverter : 
        JsonInterfaceConverter<IStorageSystem, StorageSystem, RandomStorageSystem> { }

    public class JsonIUnitConverter : JsonInterfaceConverter<IUnit, Unit, RandomUnit> { }

    public class JsonIProcessDefinitionConverter : 
        JsonInterfaceConverter<IProcessDefinition, ProcessDefinition, RandomProcessDefinition> { }

    public class JsonIProcessStepDefinitionConverter :
        JsonInterfaceConverter<IProcessStepDefinition, ProcessStepDefinition, RandomProcessStepDefinition> { }

    /// <summary>
    /// Custom DateTime converter
    /// </summary>
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateTimeFormat = "yyyy-MM-dd HH:mm"; // Format excluding seconds

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTime.ParseExact(reader.GetString() ?? string.Empty, _dateTimeFormat, CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(_dateTimeFormat));
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

        public override void Write(Utf8JsonWriter writer, ICondition value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}