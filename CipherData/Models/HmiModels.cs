using System.Text.Json.Serialization;
using System.Xml.Linq;

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

    public class CipherField
    {
        public string Path { get; set; } = string.Empty;
        public string Translation { get; set; } = string.Empty;
        public bool IsList { get; set; } = false;

        [JsonIgnore]
        public Type FieldType { get; set; } = typeof(CipherClass);
    }

    public class ReportParameter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; } = string.Empty;
        public CipherField? ParamType { get; set; }
    }

    public class Report : CipherClass
    {
        /// <summary>
        /// Report unique identifier.
        /// </summary>
        [HebrewTranslation(typeof(Report), nameof(Id))]
        public int Id { get; set; }

        /// <summary>
        /// Report title, as will be shown to user.
        /// </summary>
        [HebrewTranslation(typeof(Report), nameof(Title))]
        public string? Title { get; set; }

        /// <summary>
        /// Report creator name.
        /// </summary>
        [HebrewTranslation(typeof(Report), nameof(Creator))]
        public string Creator { get; set; } = string.Empty;

        /// <summary>
        /// Report submission date.
        /// </summary>
        [HebrewTranslation(typeof(Report), nameof(CreationDate))]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// All user-set parameters (changable by user)
        /// </summary>
        public List<ReportParameter> Parameters { get; set; } = new();

        public ObjectFactory ObjectFactory { get; set; } = new();

        [JsonIgnore]
        public Type ObjectType { get; set; } = typeof(Package);

        public string Path()
        {
            return $"Reports/{Id}";
        }

        public CheckField CheckTitle() => CheckField.Required(Title, Translate(nameof(Title)));

        public CheckField CheckCreator() => CheckField.Required(Creator, Translate(nameof(Creator)));

        public CheckField CheckCreationDate() => CheckField.Between(CreationDate, DateTime.MinValue, DateTime.Now, Translate(nameof(Creator)));

        public CheckField CheckObjectFactory() {

            Tuple<bool, string> result = ObjectFactory.Check();
            return new() { Succeeded = result.Item1, Message = result.Item2};
        }

        public CheckField CheckParameters()
        {
            if (Parameters.Any()) return CheckField.ListItems(Parameters, Translate(nameof(Parameters)));
            return new();
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckTitle());
            result.Fields.Add(CheckCreator());
            result.Fields.Add(CheckCreator());
            result.Fields.Add(CheckCreationDate());
            result.Fields.Add(CheckParameters());

            return result.Check();
        }
    }
}
