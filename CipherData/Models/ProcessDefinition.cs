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
        public string Name { get; set; }

        /// <summary>
        /// Description of process
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// All steps that are associated with this process
        /// </summary>
        public List<ProcessStepDefinition> Steps { get; set; }

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
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Name", "שם"));
            result.Add(new("Description", "תיאור"));
            result.Add(new("Steps", "שלבים"));

            return result;
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static ProcessDefinition Random(string? id = null)
        {
            string proc_name = Globals.GetRandomString(Globals.ProcessesNames);

            return new ProcessDefinition(
                    id: id,
                    name: proc_name,
                    description: proc_name,
                    steps: new List<ProcessStepDefinition>()
                );
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Fetch all processes definitions which contain the searched text
        /// </summary>
        public static Tuple<List<ProcessDefinition>?, ErrorResponse> Containing(string SearchText)
        {

            return GetObjects<ProcessDefinition>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: "ProcessDefinition.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "ProcessDefinition.Name", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "ProcessDefinition.Description", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "ProcessDefinition.Steps.Name", attributeRelation: AttributeRelation.Contains, value: SearchText,  @operator:Operator.Or)
                                                    }, @operator: Operator.Or));
        }
    }
}
