using System.Text.Json.Serialization;

namespace CipherData.Models
{
    [HebrewTranslation(nameof(ReportParameter))]
    public class ReportParameter : CipherClass
    {
        [HebrewTranslation(typeof(ReportParameter), nameof(Id))]
        public int Id { get; set; }

        [HebrewTranslation(typeof(ReportParameter), nameof(Name))]
        public string? Name { get; set; }

        [HebrewTranslation(typeof(ReportParameter), nameof(Value))]
        public string? Value { get; set; } = string.Empty;

        [HebrewTranslation(typeof(ReportParameter), nameof(ParamType))]
        public CipherField? ParamType { get; set; }

        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        public CheckField CheckValue()
        {
            CheckField result = CheckField.CheckString(Value, Translate(nameof(Value)));
            if (result.Succeeded && !string.IsNullOrEmpty(Value) && ParamType != null)
            {
                try
                {
                    // Try to convert Value to ParamType
                    var convertedValue = Convert.ChangeType(Value, ParamType.FieldType);
                    return result;
                }
                catch
                {
                    result.Succeeded = false;
                    result.Message = $"הערך {Value} של {Translate(nameof(ReportParameter))} {Name} לא תואם ל{Translate(nameof(ParamType))}.";
                }
            }
            return result;
        }

        public CheckField CheckParamType()
        {
            if (ParamType is null) return new();
            var checkRes = ParamType.Check();
            return new() { Succeeded=checkRes.Item1, Message=checkRes.Item2 };
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckValue());
            result.Fields.Add(CheckParamType());

            return result.Check();
        }
    }

    [HebrewTranslation(nameof(Report))]
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
        [HebrewTranslation(typeof(Report), nameof(Parameters))]
        public List<ReportParameter> Parameters { get; set; } = new();

        [HebrewTranslation(typeof(Report), nameof(ObjectFactory))]
        public ObjectFactory ObjectFactory { get; set; } = new();

        [JsonIgnore]
        public Type ObjectType { get; set; } = typeof(Package);

        public string Path() => $"Reports/{Id}";

        public CheckField CheckTitle() => CheckField.Required(Title, Translate(nameof(Title)));

        public CheckField CheckCreator() => CheckField.Required(Creator, Translate(nameof(Creator)));

        public CheckField CheckCreationDate() => CheckField.Between(CreationDate, DateTime.MinValue, DateTime.Now, Translate(nameof(Creator)));

        public CheckField CheckObjectFactory() {

            Tuple<bool, string> result = ObjectFactory.Check();
            return new() { Succeeded = result.Item1, Message = result.Item2};
        }

        public CheckField CheckParameters()
        {
            CheckField result = new();
            if (Parameters.Any())
            {
                result = CheckField.Distinct(Parameters.Select(x => x.Name).ToList(), Translate(nameof(Parameters)));
                if (result.Succeeded) return CheckField.ListItems(Parameters, Translate(nameof(Parameters)));
                return result;
            }
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
