using CipherData.Requests;

namespace CipherData.Models
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    public class ProcessStepDefinition: Resource
    {
        /// <summary>
        /// Unique name of the process step, two steps in the same process should not have the same name.
        /// </summary>
        [HebrewTranslation(typeof(ProcessStepDefinition), nameof(Name))]
        public string Name { get; set; }

        /// <summary>
        /// Description of the process step
        /// </summary>
        [HebrewTranslation(typeof(ProcessStepDefinition), nameof(Description))]
        public string Description { get; set; }

        /// <summary>
        /// Condition on event to be associated with the process step 
        /// </summary>
        [HebrewTranslation(typeof(ProcessStepDefinition), nameof(Condition))]
        public GroupedBooleanCondition Condition { get; set; }

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

        /// <summary>
        /// A collection of steps that make a single definition
        /// </summary>
        /// <param name="name">Unique name of the process step, two steps in the same process should not have the same name.</param>
        /// <param name="description">Description of the process step</param>
        /// <param name="condition">Condition on event to be associated with the process step </param>
        public ProcessStepDefinition(string name, string description, GroupedBooleanCondition condition, string? id = null)
        {
            Id = id ?? GetId();
            Name = name;
            Description = description;
            Condition = condition;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>s
        public string ToJson()
        {
            return ToJson(this);
        }

        /// <summary>
        /// Checks for difference between this and another object
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(ProcessStepDefinition? OtherObject)
        {

            bool different = false;

            different |= Name != OtherObject?.Name;
            different |= Description != OtherObject?.Description;

            // now for condition
            different |= Condition.Compare(OtherObject?.Condition);

            return different;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName()
        {
            return CheckField.Required(Name, Translate(nameof(Name)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription()
        {
            return CheckField.Required(Description, Translate(nameof(Description)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCondition()
        {
            Tuple<bool, string> result = Condition.Check();
            return new CheckField(result.Item1, result.Item2);
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());
            result.Fields.Add(CheckCondition());

            return result.Check();
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<ProcessStepDefinition>());

            return result.ToHashSet();
        }

        public static ProcessStepDefinition Random(string? id = null)
        {
            List<string> ProcessesStepNames = new() { "רישום", "עדכון במערכת", "השהייה" };
            string name = RandomFuncs.RandomItem(ProcessesStepNames);

            return new ProcessStepDefinition(
                id: id,
                name: name,
                description:name,
                condition: GroupedBooleanCondition.Random()
                );
        }

        /// <summary>
        /// Get an empty object scheme.
        /// </summary>
        /// <returns></returns>
        public static ProcessStepDefinition Empty()
        {
            return new ProcessStepDefinition(name: string.Empty, description: string.Empty,
                condition: GroupedBooleanCondition.Empty());
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(ProcessStepDefinition), searchedAttribute);
        }
    }
}
