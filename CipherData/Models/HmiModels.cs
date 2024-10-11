using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CipherData.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class HebrewTranslationAttribute : Attribute
    {
        public string? Translation { get; set; } = string.Empty;

        public void SetTranslation(string engWord)
        {
            if (Translator.TranslationsDictionary.ContainsKey(engWord))
            {
                Translation = Translator.TranslationsDictionary[engWord].Trim();
            }
            else
            {
                Translation = engWord.Trim();
            }
        }

        public HebrewTranslationAttribute(string engWord) => SetTranslation(engWord);

        public HebrewTranslationAttribute(Type ObjType, string engWord)
        {   
            string FullWord = (ObjType == typeof(StorageSystem)) ? $"System_{engWord}" : $"{ObjType.Name}_{engWord}";
            SetTranslation(FullWord);
        }
    }

    public class MyNavLink
    {
        public string? Name { get; set; }
        public string? Href { get; set; }
        public string? Icon { get; set; }
        public List<MySubNavLink> SubLinks { get; set; } = new();
    }

    public class MySubNavLink
    {
        public string? Name { get; set; }
        public string? Href { get; set; }
        public string? Icon { get; set; }
    }

    [HebrewTranslation(nameof(CipherField))]
    public class CipherField : CipherClass
    {
        [HebrewTranslation(typeof(CipherField), nameof(Path))]
        public string Path { get; set; } = string.Empty;

        [HebrewTranslation(typeof(CipherField), nameof(Translation))]
        public string Translation { get; set; } = string.Empty;

        [HebrewTranslation(typeof(CipherField), nameof(IsList))]
        public bool IsList { get; set; } = false;

        [JsonIgnore]
        [HebrewTranslation(typeof(CipherField), nameof(FieldType))]
        public Type FieldType { get; set; } = typeof(object);
        
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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);
    }
}
