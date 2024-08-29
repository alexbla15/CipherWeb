using CipherData.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    public class ProcessDefinition: Resource
    {
        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation("תהליך")]
        public string Name { get; set; }

        /// <summary>
        /// Description of process
        /// </summary>
        [HebrewTranslation("תיאור")]
        public string Description { get; set; }

        /// <summary>
        /// All steps that are associated with this process
        /// </summary>
        [HebrewTranslation("שלבים")]
        public List<ProcessStepDefinition> Steps { get; set; }

        /// <summary>
        /// For randomization only
        /// </summary>
        public static List<string> ProcessesNames = new() { "יצירה", "דגימה", "שינוי", "עיצוב" };

        /// <summary>
        /// Definition of a process - 
        /// a collection of steps that make a single definitio
        /// </summary>
        /// <param name="name">Name of the process</param>
        /// <param name="description">Description of process</param>
        /// <param name="steps">All steps that are associated with this process</param>
        /// <param name="id">Only if you want object to have a specific id</param>
        public ProcessDefinition(string name, string description, List<ProcessStepDefinition> steps, string? id = null)
        {
            Id = id ?? GetNextId();
            Name = name;
            Description = description;
            Steps = steps;
        }

        /// <summary>
        /// Get an empty process definition object scheme.
        /// </summary>
        /// <returns></returns>
        public static ProcessDefinition Empty()
        {
            return new ProcessDefinition(name: "", description: "", steps: new());
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId()
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

            return new ProcessDefinition(
                    id: id,
                    name: proc_name,
                    description: proc_name,
                    steps: new List<ProcessStepDefinition>()
                );
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
            return GetObjects<ProcessDefinition>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: $"{typeof(ProcessDefinition).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(ProcessDefinition).Name}.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(ProcessDefinition).Name}.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(ProcessDefinition).Name}.{nameof(Steps)}.Name", attributeRelation: AttributeRelation.Contains, value: SearchText,  @operator:Operator.Or)
                                                    }, @operator: Operator.Or));
        }
    }
}
