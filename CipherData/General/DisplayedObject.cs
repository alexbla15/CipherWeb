using System.Reflection;

namespace CipherData.General
{
    public class DisplayedProperty
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public string? Translation { get; set; }
        public object? Value { get; set; }
        public int? Order { get; set; }
    }

    public class DisplayedObject
    {
        /// <summary>
        /// All the properties of the selected object
        /// </summary>
        public List<DisplayedProperty>? Properties { get; set; }

        public List<string> UnwantedPropertiesNames { get; set; } = new();

        /// <summary>
        /// Method to get all properties of a class in a specified tuple
        /// </summary>
        /// <returns></returns>
        public DisplayedObject(object obj, List<string>? UnwantedProperties = null)
        {
            UnwantedPropertiesNames = UnwantedProperties ?? new();

            Properties = new();

            string interfaceName = obj.GetType().Name.Replace("Random", "I");
            if (!interfaceName.StartsWith("I")) interfaceName = $"I{interfaceName}";
            Type? interfaceType = Type.GetType($"CipherData.Interfaces.{interfaceName}");

            if (interfaceType != null)
            {
                MethodInfo? getDict = interfaceType.GetMethod("ToDictionary");
                Dictionary<string, object?>? values = getDict != null ? (Dictionary<string, object?>)getDict.Invoke(obj, null) : null;

                // Iterate through all the properties of the object
                if (values != null)
                {
                    foreach (string propertyName in values.Keys)
                    {
                        string propertyPath = GetPropertyPath(obj, propertyName);
                        string hebrewTranslation = GetHebrewTranslation(obj, propertyName);

                        if (!UnwantedPropertiesNames.Contains(propertyName))
                        {
                            if (values != null)
                            {
                                object? value = values.ContainsKey(propertyName) ? values[propertyName] : null;
                                Properties.Add(new() { Name = propertyName, Path = propertyPath, Translation = hebrewTranslation, Value = value });
                            }
                        }
                    }
                }
            }
        }

        public void SetOrder(string PropName, int Order)
        {
            if (Properties!=null)
            {
                foreach (var prop in Properties)
                {
                    if (prop.Name == PropName) prop.Order = Order;
                }
            }
        }

        // STATIC METHODS

        private static string GetPropertyPath(object obj, string propertyName) => $"[{obj.GetType().Name}].[{propertyName}]";

        private static string GetHebrewTranslation(object obj, string propertyName)
        {
            // Retrieve the type of the provided object
            Type objType = obj.GetType();

            string OriginalKey = $"{objType.Name}_{propertyName}";

            if (objType.IsInterface)
            {
                // Get the property information for the specified property name
                PropertyInfo? property = objType.GetProperty(propertyName);

                // Check if the property exists
                if (property == null) return OriginalKey;

                // Check if the HebrewTranslationAttribute is applied to the property
                HebrewTranslationAttribute? attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();

                return attribute?.Translation ?? OriginalKey;
            }

            Type[] interfaces = obj.GetType().GetInterfaces();

            foreach (var inter in interfaces)
            {
                OriginalKey = $"{inter.Name}_{propertyName}";

                // Get the property information for the specified property name
                PropertyInfo? property = inter.GetProperty(propertyName);

                // Check if the property exists
                if (property != null)
                {
                    // Check if the HebrewTranslationAttribute is applied to the property
                    HebrewTranslationAttribute? attribute = property.GetCustomAttribute<HebrewTranslationAttribute>();

                    return attribute?.Translation ?? OriginalKey;
                }
            }

            return OriginalKey;
        }

        public static List<DisplayedObject> ListObjects<T>(IEnumerable<T>? objects, List<string>? UnwantedCols = null)
        {
            if (objects is null) return new();
             return objects.Select(x => new DisplayedObject(x, UnwantedCols)).ToList();
        }

        public static List<Dictionary<string, object?>> ToListDicts<T>(IEnumerable<T>? objects) => ToListDicts(ListObjects(objects));

        public static List<Dictionary<string, object?>> ToListDicts(List<DisplayedObject>? objects)
        {
            List<Dictionary<string, object?>> result = new();

            if (objects is null) return result;

            foreach(var obj in objects)
            {
                Dictionary<string, object?> objDictionary = new();

                if (obj.Properties != null)
                {
                    foreach (var property in obj.Properties)
                    {
                        // Ensure the translation exists and isn't null or empty
                        if (!string.IsNullOrEmpty(property.Translation))
                        {
                            // Add translation as key and value as the corresponding value
                            objDictionary[property.Translation] = property.Value;
                        }
                    }
                }

                // Add the dictionary to the result list
                result.Add(objDictionary);
            }
            return result;
        }
    }
}
