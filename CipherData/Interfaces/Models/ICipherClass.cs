using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections;

namespace CipherData.Interfaces
{
    public interface ICipherClass
    {
        public static readonly JsonSerializerOptions JsonOptions = new()
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = {
                    new JsonIAggregateItemConverter(),
                    new JsonDateTimeConverter(),
                    new JsonIConditionConverter(),
                    new JsonICategoryConverter(),
                    new JsonICategoryPropertyConverter(),
                    new JsonIGroupedBooleanConditionConverter(),
                    new JsonIObjectFactoryConverter(),
                    new JsonIPackageConverter(),
                    new JsonIPackagePropertyConverter(),
                    new JsonIProcessDefinitionConverter(),
                    new JsonIProcessStepDefinitionConverter(),
                    new JsonIStorageSystemConverter(),
                    new JsonIVesselConverter(),
                    new JsonIUnitConverter(),
                    new JsonStringEnumConverter() ,
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) ,
                },
            };

        /// <summary>
        /// Transform this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson();

        /// <summary>
        /// Check if this object and another object of the same type are exactly the same.
        /// </summary>
        public bool Equals<T>(T? otherObject) where T : ICipherClass;

        /// <summary>
        /// Include the main interface itself
        /// </summary>
        public static IEnumerable<Type> GetInterfaces(Type type) => type.GetInterfaces().Concat(new[] { type });

        /// <summary>
        /// Method to find the interface that contains the specific property name
        /// </summary>
        public static PropertyInfo? GetPropertyInfo(Type type, string propertyName)
        {
            List<PropertyInfo> props = GetInterfaces(type)
                            .SelectMany(i => i.GetProperties())
                            .ToList();

            return props.Where(x => x.Name == propertyName).First();
        }

        /// <summary>
        /// Method to find the interface that contains the specific method name
        /// </summary>
        public static MethodInfo? GetMethodInfo(Type type, string methodName)
        {
            var a = GetInterfaces(type);
            List<MethodInfo> props = GetInterfaces(type).SelectMany(i => i.GetMethods())
                            .ToList();

            return props.Where(x => x.Name == methodName).First();
        }

        /// <summary>
        /// Translate the name of the field according to its hebrew translation. Muse give a specific type.
        /// </summary>
        public static string Translate(Type? interfaceType, string searchedAttribute)
        {
            if (interfaceType == null) return searchedAttribute;

            if (interfaceType.GetInterface(nameof(ICipherClass)) is null)
                return searchedAttribute;

            PropertyInfo? property = GetPropertyInfo(interfaceType, searchedAttribute);
            if (property == null) return searchedAttribute;

            // Get the HebrewTranslationAttribute and return the translation
            var attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();
            return attribute?.Translation ?? searchedAttribute;
        }

        /// <summary>
        /// Method to check an property of an interface, according to its Check-attribute
        /// </summary>
        /// <returns></returns>
        public static CheckField CheckProperty<T>(T CheckedObject, string propertyName) 
            where T : ICipherClass
        {
            CheckField sysError = new (false, "שגיאת מערכת");
            CheckField success = new (true, string.Empty);

            Type type = CheckedObject.GetType();

            PropertyInfo? property = GetPropertyInfo(type, propertyName);
            if (property is null) return sysError;

            var attribute = property.GetCustomAttribute<Check>();
            if (attribute is null) return sysError;

            object? value = property.GetValue(CheckedObject);

            string errorMessage = attribute.ErrorMessage ?? Translate(property.DeclaringType, propertyName);

            if (attribute.Requirement == CheckRequirement.Required)
            {
                return CheckField.Required(value?.ToString(), errorMessage, attribute.AllowedRegex);
            }
            else if (attribute.Requirement == CheckRequirement.Text)
            {
                return CheckField.CheckString(value?.ToString(), errorMessage,  attribute.AllowedRegex);
            }
            else if (attribute.Requirement == CheckRequirement.List)
            {
                if (value is null) return CheckField.Required(value, errorMessage);

                if (CipherField.IsList(value.GetType())) return CheckField.CheckList(
                    ((IEnumerable)value).Cast<object?>().Where(y => y != null).Select(y => y.ToString()).ToList(), errorMessage,
                    isFull: attribute.Full, isDistinct: attribute.Distinct, isCheckItems: attribute.CheckItems
                    );
            }
            else if (attribute.Requirement == CheckRequirement.Ne)
            {
                return (double.TryParse(value?.ToString(), out double result)) ? 
                    CheckField.NotEq(result, attribute.NumericValue, errorMessage) : sysError;
            }
            else if (attribute.Requirement == CheckRequirement.Gt)
            {
                return (decimal.TryParse(value?.ToString(), out decimal result)) ?
                    CheckField.Greater(result, decimal.Parse(attribute.NumericValue.ToString()), errorMessage) : sysError;
            }
            else if (attribute.Requirement == CheckRequirement.Ge)
            {
                return (decimal.TryParse(value?.ToString(), out decimal result)) ?
                    CheckField.GreaterEqual(result, decimal.Parse(attribute.NumericValue.ToString()), errorMessage) : sysError;
            }

            return success;
        }

        /// <summary>
        /// Transfrom Json to an object
        /// </summary>
        public static T? FromJson<T>(string json) => JsonSerializer.Deserialize<T>(json, JsonOptions);

        // generic static copy of two objected
        public static T? Copy<T>(T? obj) where T : ICipherClass
        {
            if (obj is null) return default;

            var json = obj.ToJson();
            var result = FromJson<T>(json); // Deserialize to the actual type
            return result;
        }
    }
}
