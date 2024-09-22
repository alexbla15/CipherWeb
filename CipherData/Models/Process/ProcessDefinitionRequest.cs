namespace CipherData.Models
{
    /// <summary>
    /// Create a new process definition or update it.
    /// </summary>
    public class ProcessDefinitionRequest : CipherClass
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
        /// Steps of the process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(ProcessDefinition.Steps))]
        public List<ProcessStepDefinition> Steps { get; set; } = new();

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
        public CheckField CheckSteps()
        {
            CheckField result = CheckField.FullList(Steps, Translate(nameof(Steps)));
            return (result.Succeeded) ? CheckField.ListItems(Steps, Translate(nameof(Steps))) : result;
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
            result.Fields.Add(CheckSteps());

            return result.Check();
        }

        /// <summary>
        /// Checks for difference between this and another object
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(ProcessDefinition? OtherObject)
        {

            bool different = false;

            different |= Name != OtherObject?.Name;
            different |= Description != OtherObject?.Description;

            if (Steps.Count == OtherObject?.Steps.Count)
            {
                // check for same step names
                different |= !Steps.Select(x => x.Name).ToHashSet().SetEquals(OtherObject.Steps.Select(x => x.Name).ToList());
                // check for differences
                if (!different)
                {
                    foreach (ProcessStepDefinition step in Steps)
                    {
                        different |= step.Equals(OtherObject.Steps.Where(x => x.Name == step.Name).First());
                    }
                }
            }
            else
            {
                different = true;
            }

            return different;
        }

        public ProcessDefinition Create(string id)
        {
            return new ProcessDefinition(id) { Name = Name, Description = Description, Steps=Steps };
        }
    }
}
