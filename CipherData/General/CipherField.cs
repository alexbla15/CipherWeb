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
        private string _Path { get; set; } = string.Empty;

        [HebrewTranslation(typeof(CipherField), nameof(Path))]
        public string Path {
            get => _Path;
            set { _Path = value; FieldType = GetPathType(value) ?? typeof(object); }
        }

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

        public static readonly List<AttributeRelation> AllParameterAttributeRelations = new() {
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

        public static readonly List<AttributeRelation> DateAttributeRelations = new() {
        AttributeRelation.IsNull,
        AttributeRelation.IsNotNull,
        AttributeRelation.Ne,
        AttributeRelation.Eq,
        AttributeRelation.Le,
        AttributeRelation.Lt,
        AttributeRelation.Ge,
        AttributeRelation.Gt,};

        public static readonly List<AttributeRelation> DateParameterAttributeRelations = new() {
        AttributeRelation.NeParam,
        AttributeRelation.EqParam,
        AttributeRelation.LeParam,
        AttributeRelation.LtParam,
        AttributeRelation.GeParam,
        AttributeRelation.GtParam,};

        public static readonly List<AttributeRelation> NumberAttributeRelations = new() {
        AttributeRelation.IsNull,
        AttributeRelation.IsNotNull,
        AttributeRelation.Ne,
        AttributeRelation.Eq,
        AttributeRelation.Le,
        AttributeRelation.Lt,
        AttributeRelation.Ge,
        AttributeRelation.Gt,};

        public static readonly List<AttributeRelation> NumberParameterAttributeRelations = new() {
        AttributeRelation.NeParam,
        AttributeRelation.EqParam,
        AttributeRelation.LeParam,
        AttributeRelation.LtParam,
        AttributeRelation.GeParam,
        AttributeRelation.GtParam,};

        public static readonly List<AttributeRelation> BoolAttributeRelations = new() {
        AttributeRelation.IsNull,
        AttributeRelation.IsNotNull,
        AttributeRelation.Ne,
        AttributeRelation.Eq,};

        public static readonly List<AttributeRelation> BoolParameterAttributeRelations = new() {
        AttributeRelation.NeParam, AttributeRelation.EqParam,};

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

        public static readonly List<AttributeRelation> TextParameterAttributeRelations = new() {
        AttributeRelation.StartsWithParam,
        AttributeRelation.EndsWithParam,
        AttributeRelation.NotContainsParam,
        AttributeRelation.ContainsParam,
        AttributeRelation.NeParam,
        AttributeRelation.EqParam,};

        private static List<string> GetFilters(List<AttributeRelation> relations) 
            => relations.Select(x => x.ToString()).ToList();

        // STATIC METHODS

        public static bool IsBool(Type type) => typeof(bool?).IsAssignableFrom(type);

        public static bool IsNumber(Type type) => typeof(decimal?).IsAssignableFrom(type);

        public static bool IsDateTime(Type type) => typeof(DateTime?).IsAssignableFrom(type);

        public static bool IsText(Type type) => typeof(string).IsAssignableFrom(type);

        public static bool IsList(Type type) => type is not null && type.GenericTypeArguments.Any();

        public static string Translate(string text) => ICipherClass.Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

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
                Type? rootType = GetType(parts[0]);
                if (rootType is null) return null;

                string translation = $"[{Translator.TranslateText(rootType.Name)}]";

                foreach (var part in parts.GetRange(1, parts.Count-1))
                {
                    translation += $".[{Translator.TranslateProperty(rootType, part)}]";
                    rootType = GetPartPathType(rootType, part);
                }

                return translation;
            }

            return null;
        }

        /// <summary>
        /// Method to translate the path, and take only the property's translation
        /// </summary>
        public static string? TranslatePropertyFromPath(string? path)
            => TranslatePath(path)?.Trim('[', ']').Split("].[").Last();

        public static PropertyInfo? GetPropertyInfo(Type? type, string prop)
            => type?.GetInterfaces() // Get all interfaces that this type inherits
                    .Append(type) // Include this type itself
                    .Select(i => i.GetProperty(prop)) // Check each interface for the property name
                    .FirstOrDefault(p => p != null); // Find the first non-null result

        public static Dictionary<string, AttributeRelation> RelationTranslationMapping(bool WithParameters = false)
        {
            var selectedAttributes = WithParameters ? AllAttributeRelations.Concat(AllParameterAttributeRelations) : AllAttributeRelations;

            return selectedAttributes.Select(x => KeyValuePair.Create(Translator.TranslateText(x.ToString()), x))
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static List<string> SpecificAttributeRelations(
            List<AttributeRelation> regRelations, List<AttributeRelation> paramRelations, bool WithParameters = false)
            => WithParameters ?
                GetFilters(regRelations).Concat(GetFilters(paramRelations)).ToList() : GetFilters(regRelations);

        public static Dictionary<FilterType, List<string>> FilterAttributeRelations(bool WithParameters = false)
            => new()
            {
                [FilterType.Number] = SpecificAttributeRelations(NumberAttributeRelations, NumberParameterAttributeRelations, WithParameters),
                [FilterType.Text] = SpecificAttributeRelations(TextAttributeRelations, TextParameterAttributeRelations, WithParameters),
                [FilterType.Date] = SpecificAttributeRelations(DateAttributeRelations, DateParameterAttributeRelations, WithParameters),
                [FilterType.Condition] = SpecificAttributeRelations(BoolAttributeRelations, BoolParameterAttributeRelations, WithParameters)
            };

        public static FilterType GetFilterType(CipherField field)
        {
            if (field.IsDateTime()) return FilterType.Date;
            if (field.IsBool()) return FilterType.Condition;
            if (field.IsNumber()) return FilterType.Number;
            if (field.IsText()) return FilterType.Text;
            if (field.IsList()) return GetFilterType(new CipherField() { FieldType = field.ItemType() });
            return FilterType.Other;
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

        public static Type ItemType(Type type) => IsList(type) ? type.GenericTypeArguments[0] : type;

        public static Type? GetPathType(Type? rootType, string path)
        {
            Type? res = rootType;
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

        public static Type? GetPathType(string path)
        {
            var parts = path.Trim('[', ']').Split("].[");
            Type? rootType = GetType(parts[0]);
            return GetPathType(rootType, path);
        }

        public static Type? GetPartPathType(Type? currType, string prop)
        {
            PropertyInfo? propInfo = currType?.GetProperty(prop);
            Type? res = propInfo?.PropertyType;
            return res != null &&  IsList(res) ? ItemType(res) : res;
        }

        /// <summary>
        /// Method to get a type in Cipher-context using its name
        /// Transforms Random-types / api-types to interfaces.
        /// </summary>
        public static Type? GetType(string typeName)
        {
            string interfaceName = typeName.Replace("Random", "I");
            if (!interfaceName.StartsWith("I")) interfaceName = $"I{interfaceName}";
            return GetInterfaceType(interfaceName) ?? GetGeneralType(typeName);
        }

        public static Type? GetInterfaceType(string typeName) => Type.GetType($"CipherData.Interfaces.{typeName}");

        public static Type? GetGeneralType(string typeName) => Type.GetType($"CipherData.General.{typeName}");

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

        /// <summary>
        /// retrieves all types from the assembly containing the Resource class that directly inherit from Resource
        /// </summary>
        public static List<Type> GetSubClasses(Type type)
            => type.Assembly.GetTypes().Where(x => x.BaseType?.Name == type.Name).ToList();

        /// <summary>
        /// Create a CipherField out of a type
        /// </summary>
        /// <param name="type"></param>
        public static CipherField Create(Type type) => 
            new() { Path = type.Name, Translation = Translator.TranslateText(type.Name), FieldType = type };

        /// <summary>
        /// Get all cipher field of the first layer of some type 
        /// (without going into deeper layers, e.g. Package -> Package.Category but not Package.Category.Id)
        /// </summary>
        /// <param name="type">desired type for field-search</param>
        /// <param name="mainPath">tree-path to the desired field</param>
        /// <param name="mainTranslation">translation of tree-path</param>
        public static List<CipherField> GetFields_SingleLayer(Type type, string? mainPath = null, string? mainTranslation = null)
        {
            List<PropertyInfo> fields = type.GetProperties().Where(x => Translator.HasHebrewTranslator(x)).ToList();

            return fields.Select(x => new CipherField()
            {
                FieldType = x.PropertyType,

                Translation = mainTranslation != null ? $"{mainTranslation}.[{Translator.TranslateProperty(x)}]" :
                $"[{Translator.TranslateType(type)}].[{Translator.TranslateProperty(x)}]",
                Path = mainPath != null ? $"{mainPath}.[{x.Name}]" : $"[{type.Name}].[{x.Name}]"
            }).ToList();
        }

        /// <summary>
        /// Get a translation list (english,hebrew) of all available field of a Cipher data model.
        /// </summary>
        public static List<CipherField> GetFields(Type setType, string? mainPath = null, string? mainTranslation = null, int curr_depth = 0)
        {
            int max_depth = 2;

            List<CipherField> fields = GetFields_SingleLayer(setType, mainPath, mainTranslation);
            List<CipherField> new_fields = new();

            if (curr_depth <= max_depth)
            {
                foreach (CipherField field in fields)
                {
                    var types = GetSubClasses(typeof(ICipherClass));

                    if (types.Contains(field.FieldType))
                    {
                        new_fields.AddRange(GetFields(setType, mainPath: $"{field.Path}",
                        mainTranslation: $"{field.Translation}", curr_depth + 1));
                    }
                    else if (field.IsList())
                    {
                        List<CipherField> addition_fields = GetFields(field.FieldType.GetGenericArguments()[0], mainPath: $"{field.Path}",
                        mainTranslation: $"{field.Translation}", curr_depth + 1);
                        new_fields.AddRange(addition_fields);
                    }
                }
            }

            fields.AddRange(new_fields);
            return fields;
        }
    }
}
