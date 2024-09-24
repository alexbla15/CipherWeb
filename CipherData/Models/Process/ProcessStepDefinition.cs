using CipherData.Randomizer;

namespace CipherData.Models
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    public class ProcessStepDefinition: Resource
    {
        private string _Name = string.Empty;

        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string Name
        {
            get { return _Name; }
            set { _Name = value.Trim(); }
        }

        private string _Description = string.Empty;

        /// <summary>
        /// Description of process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        public string Description
        {
            get { return _Description; }
            set { _Description = value.Trim(); }
        }
        /// <summary>
        /// Condition on event to be associated with the process step 
        /// </summary>
        [HebrewTranslation(typeof(ProcessStepDefinition), nameof(Condition))]
        public GroupedBooleanCondition Condition { get; set; } = new();

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

        /// <summary>
        /// A collection of steps that make a single definition
        /// </summary>
        public ProcessStepDefinition(string? id = null)
        {
            Id = id ?? GetId();
        }

        /// <summary>
        /// Check if this object and other object are exactly the same
        /// </summary>
        public bool Equals(ProcessStepDefinition? OtherObject)
        {
            if (OtherObject is null) return false;
            if (Id != OtherObject.Id) return false;
            if (Name != OtherObject.Name) return false;
            if (!Condition.Equals(OtherObject.Condition)) return false;

            return true;
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

        public static ProcessStepDefinition Random(string? id = null)
        {
            List<string> ProcessesStepNames = new() { "רישום", "עדכון במערכת", "השהייה" };
            string name = RandomFuncs.RandomItem(ProcessesStepNames);

            return new ProcessStepDefinition(id)
            {
                Name = name,
                Description = name,
                Condition = GroupedBooleanCondition.Random()
            };
        }
    }
}
