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
        public object OriginalObject { get; set; }

        /// <summary>
        /// All the properties of the selected object
        /// </summary>
        public List<DisplayedProperty>? Properties { get; set; }

        public List<string> UnwantedPropertiesNames { get; set; } = new();

        /// <summary>
        /// Method to get all properties of a class in a specified tuple
        /// </summary>
        public DisplayedObject(object obj, List<string>? UnwantedProperties = null)
        {
            OriginalObject = obj;

            UnwantedPropertiesNames = UnwantedProperties ?? new();

            Properties = new();

            string originalName = obj.GetType().Name;
            Type? ChosenType = CipherField.GetType(originalName);

            if (ChosenType != null)
            {
                MethodInfo? getDict = ChosenType.GetMethod("ToDictionary");
                Dictionary<string, object?>? values = getDict != null ? (Dictionary<string, object?>)getDict.Invoke(obj, null) : null;

                // Iterate through all the properties of the object
                if (values != null)
                {
                    foreach (string propertyName in values.Keys)
                    {
                        string propertyPath = GetPropertyPath(ChosenType, propertyName);
                        string? hebrewTranslation = CipherField.TranslatePropertyFromPath(propertyPath);

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

        // STATIC METHODS

        private static string GetPropertyPath(Type type, string propertyName) => 
            $"[{type.Name}].[{propertyName}]";

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
