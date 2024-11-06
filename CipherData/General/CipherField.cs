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

        public bool IsList() => FieldType.GenericTypeArguments.Any();

        public Type ItemType() => IsList() ? FieldType.GenericTypeArguments[0] : FieldType;

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

        public static string Translate(string text) => ICipherClass.Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
        
        
    }
}
