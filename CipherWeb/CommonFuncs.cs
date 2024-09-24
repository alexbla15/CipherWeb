using System.Reflection;
using CipherData.Models;
using CipherWeb.Shared.Components.CipherSpecific;
using static CipherWeb.Shared.Components.CipherSpecific.CipherBooleanConditions;

namespace CipherWeb
{
    public class CommonFuncs
    {
        /// <summary>
        /// Get a list of all available field within a Cipher data model.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string[] GetFields(Type a)
        {
            FieldInfo[] fields = a.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            List<string> fields_names = new();
            foreach (FieldInfo field in fields)
            {
                fields_names.Add(field.Name.Replace("k__BackingField", "").Replace("<","").Replace(">",""));
            }
            return fields_names.ToArray();
        }   

        /// <summary>
        /// Get a translation list (english,hebrew) of all available field of a Cipher data model.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static List<Tuple<string, string>> GetTranslatedFields(Type a)
        {
            string[] fields = GetFields(a);
            List<Tuple<string, string>> hebFields = new();
            foreach (Tuple<string, string> h in HebrewDictionary.Headers)
            {
                if (fields.Contains(h.Item1))
                {
                    hebFields.Add(h);
                }
            }
            return hebFields;
        }

        /// <summary>
        /// Get english-translation of a hebrew word.
        /// </summary>
        /// <param name="heb_field"></param>
        /// <returns></returns>
        public static string? DeTranslateField(string heb_field)
        {
            foreach (Tuple<string, string> h in HebrewDictionary.Headers)
            {
                if (h.Item2 == heb_field)
                {
                    return h.Item1;
                }
            }
            return null;
        }

        /// <summary>
        /// Method to get all classes that inherit from CipherClass
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetCipherClasses()
        {
            return typeof(CipherClass).Assembly.GetTypes().Where(x=>x.BaseType?.Name == nameof(CipherClass)).ToList();
        }

        /// <summary>
        /// Get all cipher field of the first layer of some type 
        /// (without going into deeper layers, e.g. Package -> Package.Category but not Package.Category.Id)
        /// </summary>
        /// <param name="type">desired type for field-search</param>
        /// <param name="mainPath">tree-path to the desired field</param>
        /// <param name="mainTranslation">translation of tree-path</param>
        public static List<CipherField> GetCipherTypeFields_SingleLayer(Type type, string? mainPath = null, string? mainTranslation = null)
        {
            List<PropertyInfo> fields = type.GetProperties().Where(x => x.GetCustomAttribute<HebrewTranslationAttribute>() != null).ToList();
            return fields.Select(x => new CipherField()
            {
                FieldType = x.PropertyType,
                Translation = mainTranslation != null ? $"{mainTranslation}.[{x.GetCustomAttribute<HebrewTranslationAttribute>().Translation}]" : $"[{x.GetCustomAttribute<HebrewTranslationAttribute>().Translation}]",
                Path = mainPath != null ? $"{mainPath}.[{type.Name}].[{x.Name}]" : $"[{type.Name}].[{x.Name}]", IsList = x.PropertyType.IsGenericType
            }).ToList();
        }

        /// <summary>
        /// Get a translation list (english,hebrew) of all available field of a Cipher data model.
        /// </summary>
        public static List<CipherField> GetCipherTypeFields(Type setType, string? mainPath = null, string? mainTranslation = null, int curr_depth = 0)
        {
            int max_depth = 2;

            List<CipherField> fields = GetCipherTypeFields_SingleLayer(setType, mainPath, mainTranslation);
            List<CipherField> new_fields = new();

            if (curr_depth <= max_depth)
            {
                foreach (CipherField field in fields)
                {
                    if (CachedData.CipherTypes.Contains(field.FieldType))
                    {
                        new_fields.AddRange(GetCipherTypeFields(setType, mainPath: $"[{field.Path}]",
                        mainTranslation: $"{field.Translation}", curr_depth+1));
                    }
                    else if (field.IsList)
                    {
                        new_fields.AddRange(GetCipherTypeFields(field.FieldType.GetGenericArguments()[0], mainPath: $"[{field.Path}]",
                        mainTranslation: $"{field.Translation}", curr_depth+1));
                    }
                }
            }

            fields.AddRange(new_fields);
            return fields;
        }
    }
}
