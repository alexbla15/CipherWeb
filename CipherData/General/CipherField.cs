using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CipherData.General
{
    [HebrewTranslation(nameof(CipherField))]
    public class CipherField : CipherClass, ICipherClass
    {
        [HebrewTranslation(typeof(CipherField), nameof(Path))]
        public string Path { get; set; } = string.Empty;

        [HebrewTranslation(typeof(CipherField), nameof(Translation))]
        public string Translation { get; set; } = string.Empty;

        [JsonIgnore]
        [HebrewTranslation(typeof(CipherField), nameof(FieldType))]
        public Type FieldType { get; set; } = typeof(object);

        public bool IsList() => IsList(FieldType);

        public Type ItemType() => ItemType(FieldType);

        public CheckField CheckPath()
        {
            CheckField result = CheckField.Required(Path, Translate(nameof(Path)), AllowedRegex: @"^[a-zA-Z0-9.\[\] \n?]+$");
            if (result.Succeeded)
            {
                /// regex pattern: [--component1--].[--component2--].[--component3--].[--component4--]
                string pattern = @"^\[([a-zA-Z0-9\-,. ]+)\](\.\[([a-zA-Z0-9\-,. ]+)\])*$";
                result.Succeeded = Regex.IsMatch(Path, pattern);
                if (!result.Succeeded)
                {
                    result.Message = $"שגיאת מערכת. השדה {Translate(nameof(Path))} לא תואם לתבנית הדרושה.";
                }
            }
            return result;
        }

        public CheckField CheckTranslation()
        {
            CheckField result = CheckField.Required(Translation, Translate(nameof(Translation)), AllowedRegex: @"^[a-zA-Z0-9א-ת.\[\] \n?]+$");
            if (result.Succeeded)
            {
                /// regex pattern: [--component1--].[--component2--].[--component3--].[--component4--]
                string pattern = @"^\[([a-zA-Z0-9א-ת\-,. ]+)\](\.\[([a-zA-Z0-9א-ת\-,. ]+)\])*$";
                result.Succeeded = Regex.IsMatch(Translation, pattern);
                if (!result.Succeeded)
                {
                    result.Message = $"שגיאת מערכת. השדה {Translate(nameof(Translation))} לא תואם לתבנית הדרושה.";
                }
            }
            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckPath());
            result.Fields.Add(CheckTranslation());

            return result.Check();
        }

        // STATIC DATA

        public static readonly List<string> AllFilters = new() {
        nameof(AttributeRelation.IsNull),
        nameof(AttributeRelation.IsNotNull),
        nameof(AttributeRelation.StartsWith),
        nameof(AttributeRelation.EndsWith),
        nameof(AttributeRelation.IsEmpty),
        nameof(AttributeRelation.IsNotEmpty),
        nameof(AttributeRelation.NotContains),
        nameof(AttributeRelation.Contains),
        nameof(AttributeRelation.Ne),
        nameof(AttributeRelation.Eq),
        nameof(AttributeRelation.Le),
        nameof(AttributeRelation.Lt),
        nameof(AttributeRelation.Ge),
        nameof(AttributeRelation.Gt),};

        public static readonly List<string> DateFilters = new() {
        nameof(AttributeRelation.IsNull),
        nameof(AttributeRelation.IsNotNull),
        nameof(AttributeRelation.Ne),
        nameof(AttributeRelation.Eq),
        nameof(AttributeRelation.Le),
        nameof(AttributeRelation.Lt),
        nameof(AttributeRelation.Ge),
        nameof(AttributeRelation.Gt),};

        public static readonly List<string> NumberFilters = new() {
        nameof(AttributeRelation.IsNull),
        nameof(AttributeRelation.IsNotNull),
        nameof(AttributeRelation.Ne),
        nameof(AttributeRelation.Eq),
        nameof(AttributeRelation.Le),
        nameof(AttributeRelation.Lt),
        nameof(AttributeRelation.Ge),
        nameof(AttributeRelation.Gt),};

        public static readonly List<string> BoolFilters = new() {
        nameof(AttributeRelation.IsNull),
        nameof(AttributeRelation.IsNotNull),
        nameof(AttributeRelation.Ne),
        nameof(AttributeRelation.Eq),};

        public static readonly List<string> TextFilters = new() {
        nameof(AttributeRelation.IsNull),
        nameof(AttributeRelation.IsNotNull),
        nameof(AttributeRelation.StartsWith),
        nameof(AttributeRelation.EndsWith),
        nameof(AttributeRelation.IsEmpty),
        nameof(AttributeRelation.IsNotEmpty),
        nameof(AttributeRelation.NotContains),
        nameof(AttributeRelation.Contains),
        nameof(AttributeRelation.Ne),
        nameof(AttributeRelation.Eq),};

        // STATIC METHODS

        public static bool IsList(Type type) => type.GenericTypeArguments.Any();

        public static Type ItemType(Type type) => IsList(type) ? type.GenericTypeArguments[0] : type;

        public static string Translate(string text) => ICipherClass.Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        public static string? TranslatePartPath(Type? currType, string prop)
        {
            PropertyInfo? propInfo = currType?.GetProperty(prop);
            if (propInfo != null)
            {
                HebrewTranslationAttribute? hebAtt = propInfo.GetCustomAttribute<HebrewTranslationAttribute>();
                if (hebAtt != null) return hebAtt.Translation;
            }

            return prop;
        }

        public static Type? GetPartPathType(Type? currType, string prop)
        {
            PropertyInfo? propInfo = currType?.GetProperty(prop);
            Type? res = propInfo?.PropertyType;
            return res != null &&  IsList(res) ? ItemType(res) : res;
        }

        public static Type? GetInterfaceType(string typeName) => Type.GetType($"CipherData.Interfaces.{typeName}");

        /// <summary>
        /// Method to get the translation of a property-path with a specific scheme [Root].[Prop].[]...
        /// </summary>
        public static string? TranslatePath(string? path)
        {
            if (path is null) return null;

            // remove start and end of the attribute, and split by splitters
            List<string> parts = path.Trim('[', ']').Split("].[").ToList();

            if (parts.Any())
            {
                Type? rootType = GetInterfaceType(parts[0]);
                if (rootType is null) return null;

                string translation = $"[{Translator.GetTranslation(rootType.Name)}]";

                foreach (var part in parts.GetRange(1, parts.Count-1))
                {
                    translation += $".[{TranslatePartPath(rootType, part)}]";
                    rootType = GetPartPathType(rootType, part);
                }

                return translation;
            }

            return null;
        }
    }
}
