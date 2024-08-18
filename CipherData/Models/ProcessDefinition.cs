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

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

        /// <summary>
        /// Definition of a process - 
        /// a collection of steps that make a single definitio
        /// </summary>
        /// <param name="name">Name of the process</param>
        /// <param name="description">Description of process</param>
        /// <param name="steps">All steps that are associated with this process</param>
        public ProcessDefinition(string name, string description, List<ProcessStepDefinition> steps, string? id = null)
        {
            Id = id ?? GetId();
            Name = name;
            Description = description;
            Steps = steps;
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
    }
}
