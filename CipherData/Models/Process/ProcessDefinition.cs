using CipherData.Requests;

namespace CipherData.Models
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    public class ProcessDefinition: Resource
    {
        private string _Name = string.Empty;

        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string Name {
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
        /// All steps that are associated with this process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Steps))]
        public List<ProcessStepDefinition> Steps { get; set; } = new();

        /// <summary>
        /// For randomization only
        /// </summary>
        public static readonly List<string> ProcessesNames = new() { "יצירה", "דגימה", "שינוי", "עיצוב" };

        /// <summary>
        /// Definition of a process - 
        /// a collection of steps that make a single definitio
        /// </summary>
        /// <param name="id">Only if you want object to have a specific id</param>
        public ProcessDefinition(string? id = null)
        {
            Id = id ?? GetNextId();
        }

        /// <summary>
        /// Check if this object and other object are exactly the same
        /// </summary>
        public bool Equals(ProcessDefinition? OtherObject)
        {
            if (OtherObject is null) return false;
            if (Id != OtherObject.Id) return false;
            if (Name != OtherObject.Name) return false;
            if (Description != OtherObject.Description) return false;

            if (Steps.Count != OtherObject.Steps.Count) return false;
            if (Steps.Any())
            {
                foreach (ProcessStepDefinition step in Steps.OrderBy(x => x.Id))
                {
                    if (!step.Equals(OtherObject.Steps[Steps.IndexOf(step)])) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId()
        {
            IdCounter += 1;
            return $"PD{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<ProcessDefinition>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static ProcessDefinition Random(string? id = null)
        {

            string proc_name = RandomFuncs.RandomItem(ProcessesNames);

            return new ProcessDefinition(id)
            {
                Name = proc_name,
                Description = proc_name,
                Steps = new() { ProcessStepDefinition.Random() }
            };
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(ProcessDefinition), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<ProcessDefinition>, ErrorResponse> All()
        {
            return ProcessDefinitionsRequests.GetProcessDefinitions();
        }

        /// <summary>
        /// Fetch all processes definitions which contain the searched text
        /// </summary>
        public static Tuple<List<ProcessDefinition>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<ProcessDefinition>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Name)}", Value = SearchText },
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Description)}", Value = SearchText},
                new() { Attribute = $"{typeof(ProcessDefinition).Name}.{nameof(Steps)}.{nameof(ProcessStepDefinition.Name)}", Value = SearchText, Operator = Operator.Any }
                }, Operator = Operator.Any 
            });
        }
    }
}
