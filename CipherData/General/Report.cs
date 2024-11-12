using CipherData.Interfaces;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CipherData.General
{
    [HebrewTranslation(nameof(ReportParameter))]
    public class ReportParameter : CipherClass, ICipherClass
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

        // STATIC METHODS

        public static string Translate(string text) => ICipherClass.Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    [HebrewTranslation(nameof(Report))]
    public class Report : CipherClass, ICipherClass
    {
        /// <summary>
        /// Report version.
        /// </summary>
        [HebrewTranslation(typeof(Report), nameof(Version))]
        public int Version { get; set; } = 1;

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
        /// All user-set parameters (changeable by user)
        /// </summary>
        [HebrewTranslation(typeof(Report), nameof(Parameters))]
        public List<ReportParameter> Parameters { get; set; } = new();

        [HebrewTranslation(typeof(Report), nameof(ObjectFactory))]
        public IObjectFactory ObjectFactory { get; set; } = new ObjectFactory();

        [JsonIgnore]
        public Type ObjectType { get; set; } = typeof(IPackage);

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
            result.Fields.Add(CheckObjectFactory());

            return result.Check();
        }

        public async Task<Tuple<bool, string>> ExistsInDb(ICipherInfo db, bool ShouldExist = false)
        {
            bool exists_by_title = await db.ExistsInDb(this);
            bool exists_by_id = await db.ExistsInDb(this, CheckTitle:false);

            if (exists_by_title && !ShouldExist) return Tuple.Create(false, $"דוח בשם {Title} כבר קיים. נא להחליף שם או לעדכן גרסה של הדוח בעמוד הייעודי לכך.");

            if (!exists_by_id && ShouldExist) return Tuple.Create(false, $"דוח מספר {Id} לא קיים.");

            return Tuple.Create(true, string.Empty);
        }

        public bool Different(Report? otherReport)
        {
            if (otherReport == null) return true;
            if (Id != otherReport.Id) return true;
            if (Title != otherReport.Title) return true;
            if (Creator != otherReport.Creator) return true;

            if (!Parameters.SequenceEqual(otherReport.Parameters)) return true;

            if (otherReport.ObjectFactory is ObjectFactory obj)
            {
                if (!ObjectFactory.Equals(obj)) return true;
            }

            return false;
        }

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Version)] = Version,
                [nameof(Title)] = Title,
                [nameof(Creator)] = Creator,
                [nameof(CreationDate)] = CreationDate,
            };
        }

        public Report Export()
        {
            Report? newItem = new()
            {
                CreationDate = CreationDate,
                Creator = Creator,
                Parameters = Parameters,
                Id = Id,
                Title = Title,
                Version = Version,
                ObjectType = ObjectType,
                ObjectFactory = ObjectFactory.Export()
            };

            return newItem;
        }

        public new string ToJson()
            => JsonSerializer.Serialize(Export(), typeof(Report), ICipherClass.JsonOptions);

        // STATIC METHODS

        public static string Translate(string text) => 
            ICipherClass.Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        public static async Task<Report> Get(ICipherInfo db, int Id) => await db.GetReport(Id);

        public static async Task<List<Report>> All(ICipherInfo db) => await db.GetAllUpdatedReports();
    }
}
