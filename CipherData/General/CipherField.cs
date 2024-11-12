using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CipherData.General
{
    public enum FilterType
    {
        Text, Number, Date, Condition, Other
    }

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
        public FilterType GetFilterType() => GetFilterType(this);

        public bool IsBool() => IsBool(FieldType);

        public bool IsNumber() => IsNumber(FieldType);

        public bool IsDateTime() => IsDateTime(FieldType);

        public bool IsText() => IsText(FieldType);

        // STATIC DATA

        public static readonly List<AttributeRelation> AllAttributeRelations = new() {
        AttributeRelation.IsNull, AttributeRelation.IsNotNull,
        AttributeRelation.StartsWith,
        AttributeRelation.EndsWith,
        AttributeRelation.IsEmpty,
        AttributeRelation.IsNotEmpty,
        AttributeRelation.NotContains,
        AttributeRelation.Contains,
        AttributeRelation.Ne,
        AttributeRelation.Eq,
        AttributeRelation.Le,
        AttributeRelation.Lt,
        AttributeRelation.Ge,
        AttributeRelation.Gt,};

        public static readonly List<AttributeRelation> DateAttributeRelations = new() {
        AttributeRelation.IsNull,
        AttributeRelation.IsNotNull,
        AttributeRelation.Ne,
        AttributeRelation.Eq,
        AttributeRelation.Le,
        AttributeRelation.Lt,
        AttributeRelation.Ge,
        AttributeRelation.Gt,};

        public static readonly List<AttributeRelation> NumberAttributeRelations = new() {
        AttributeRelation.IsNull,
        AttributeRelation.IsNotNull,
        AttributeRelation.Ne,
        AttributeRelation.Eq,
        AttributeRelation.Le,
        AttributeRelation.Lt,
        AttributeRelation.Ge,
        AttributeRelation.Gt,};

        public static readonly List<AttributeRelation> BoolAttributeRelations = new() {
        AttributeRelation.IsNull,
        AttributeRelation.IsNotNull,
        AttributeRelation.Ne,
        AttributeRelation.Eq,};

        public static readonly List<AttributeRelation> TextAttributeRelations = new() {
        AttributeRelation.IsNull,
        AttributeRelation.IsNotNull,
        AttributeRelation.StartsWith,
        AttributeRelation.EndsWith,
        AttributeRelation.IsEmpty,
        AttributeRelation.IsNotEmpty,
        AttributeRelation.NotContains,
        AttributeRelation.Contains,
        AttributeRelation.Ne,
        AttributeRelation.Eq,};

        public static readonly List<AttributeRelation> ParameterAttributeRelations = new() {
        AttributeRelation.StartsWithParam,
        AttributeRelation.EndsWithParam,
        AttributeRelation.NotContainsParam,
        AttributeRelation.ContainsParam,
        AttributeRelation.NeParam,
        AttributeRelation.EqParam,
        AttributeRelation.LeParam,
        AttributeRelation.LtParam,
        AttributeRelation.GeParam,
        AttributeRelation.GtParam,};

        public static readonly List<string> AllFilters = 
            AllAttributeRelations.Select(x=>x.ToString()).ToList();

        public static readonly List<string> DateFilters =
            DateAttributeRelations.Select(x => x.ToString()).ToList();

        public static readonly List<string> NumberFilters =
            NumberAttributeRelations.Select(x => x.ToString()).ToList();

        public static readonly List<string> BoolFilters =
            BoolAttributeRelations.Select(x => x.ToString()).ToList();

        public static readonly List<string> TextFilters =
            TextAttributeRelations.Select(x => x.ToString()).ToList();

        public static readonly List<string> ParameterFilters =
            ParameterAttributeRelations.Select(x => x.ToString()).ToList();

        // STATIC METHODS

        public static Dictionary<string, AttributeRelation> RelationTranslationMapping(bool WithParameters = false)
        {
            var selectedAttributes = WithParameters ? AllAttributeRelations.Concat(ParameterAttributeRelations) : AllAttributeRelations;

            return selectedAttributes.Select(x => KeyValuePair.Create(Translator.GetTranslation(x.ToString()), x))
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static Dictionary<FilterType, List<string>> FilterAttributeRelations(bool WithParameters = false)
        {
            return new()
            {
                [FilterType.Number] = WithParameters ? NumberFilters.Concat(ParameterFilters).ToList() : NumberFilters,
                [FilterType.Text] = WithParameters ? TextFilters.Concat(ParameterFilters).ToList() : TextFilters,
                [FilterType.Date] = WithParameters ? DateFilters.Concat(ParameterFilters).ToList() : DateFilters,
                [FilterType.Condition] = WithParameters ? BoolFilters.Concat(ParameterFilters).ToList() : BoolFilters,
            };
        }

        public static bool IsBool(Type type) => typeof(bool?).IsAssignableFrom(type);

        public static bool IsNumber(Type type) => typeof(decimal?).IsAssignableFrom(type);

        public static bool IsDateTime(Type type) => typeof(DateTime?).IsAssignableFrom(type);

        public static bool IsText(Type type) => typeof(string).IsAssignableFrom(type);

        public static FilterType GetFilterType(CipherField field)
        {
            if (field.IsDateTime()) return FilterType.Date;
            if (field.IsBool()) return FilterType.Condition;
            if (field.IsNumber()) return FilterType.Number;
            if (field.IsText()) return FilterType.Text;
            if (field.IsList()) return GetFilterType(new CipherField() { FieldType = field.ItemType() });
            return FilterType.Other;
        }

        public static bool IsList(Type type) => (type is null) ? false : type.GenericTypeArguments.Any();

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

        public static Type? GetPathType(Type? rootType, string path)
        {
            Type res = rootType;
            var parts = path.Trim('[', ']').Split("].[");

            if (parts.Length > 1)
            {
                res = GetPartPathType(rootType, parts[1]);
                if (res == null) return null;
                if (IsText(res) || IsNumber(res) || IsDateTime(res) || IsDateTime(res)) 
                    return res;
                return GetPathType(res, string.Join("].[", parts.ToList().GetRange(1, parts.Count() - 1)));
            }
            return res;
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

        public static List<AttributeRelation> GetFilters(CipherField field)
        {
            if (typeof(bool?).IsAssignableFrom(field.FieldType)) return BoolAttributeRelations;
            if (typeof(DateTime?).IsAssignableFrom(field.FieldType)) return DateAttributeRelations;
            if (typeof(decimal?).IsAssignableFrom(field.FieldType)) return NumberAttributeRelations;
            if (typeof(string).IsAssignableFrom(field.FieldType)) return TextAttributeRelations;
            if (field.IsList()) return GetFilters(new CipherField() { FieldType = field.ItemType() });
            else return AllAttributeRelations;
        }

        public static List<Type> InterfaceChildren(Type mainInterface)
        {
            List<Type> children = new();

            if (mainInterface.IsInterface)
            {
                children = mainInterface.Assembly.GetTypes().
                    Where(x => x.IsInterface && x != mainInterface && x.IsAssignableTo(mainInterface))
                    .ToList();
            }

            return children;
        }
    }
}
