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
        public DisplayedObject(object obj, List<string>? UnwantedProperties = null, List<Tuple<string,int>>? OrderProperties = null)
        {
            UnwantedPropertiesNames = UnwantedProperties ?? new();

            Properties = new();

            string interfaceName = obj.GetType().Name.Replace("Random", "I");
            Type? interfaceType = Type.GetType($"CipherData.Models.{interfaceName}");

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
            string originalKey = $"{obj.GetType().Name}_{propertyName}";

            // deal specifically with StorageSystem
            originalKey = originalKey.Replace("StorageSystem", "System").Replace("Random","");

            string translation = Translator.GetTranslation(originalKey);
            if (translation != originalKey) return translation;

            // If not successful, check for a base (parent) class
            Type? baseType = obj.GetType().BaseType;
            if (baseType != null && baseType != typeof(object))
            {
                // Generate the translation key for the base class
                string baseClassKey = $"{baseType.Name}_{propertyName}";

                // Try to get the translation for the base class
                string baseTranslation = Translator.GetTranslation(baseClassKey);

                // If the translation was successful for the base class, return it
                if (baseTranslation != baseClassKey) return baseTranslation;
            }
            return translation;
        }

        public static List<DisplayedObject> ListObjects<T>(IEnumerable<T>? objects, List<string>? UnwantedCols = null, List<Tuple<string, int>>? OrderCols = null)
        {
            if (objects is null) return new();
             return objects.Select(x => new DisplayedObject(x, UnwantedCols, OrderCols)).ToList();
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
