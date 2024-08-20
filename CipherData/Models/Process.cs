using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    public class Process: Resource
    {
        /// <summary>
        /// a collection of steps that make a single definition
        /// </summary>
        public ProcessDefinition Definition { get; set; }

        /// <summary>
        /// Events taking place during a process
        /// </summary>
        public HashSet<Event> Events { get; set; }

        /// <summary>
        /// Uncompleted steps for completing the process
        /// </summary>
        public HashSet<ProcessStepDefinition> UncompletedSteps { get; set; }

        /// <summary>
        /// An instance of a specific processes
        /// </summary>
        /// <param name="definition">a collection of steps that make a single definition</param>
        /// <param name="events">Events taking place during a process</param>
        /// <param name="uncompletedSteps">Uncompleted steps for completing the process</param>
        /// <param name="id">Only if you want process to have specific id</param>
        public Process(ProcessDefinition definition, HashSet<Event> events, HashSet<ProcessStepDefinition> uncompletedSteps,
            string? id = null)
        {
            Id = id ?? GetNextId();
            Definition = definition;
            Events = events;
            UncompletedSteps = uncompletedSteps;

            Start = Events.Select(x => x.Timestamp).Min();
            End = Events.Select(x => x.Timestamp).Max();
        }

        public DateTime Start;
        public DateTime End;

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
            return $"PR{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Definition", "הגדרה"));
            result.Add(new("Events", "תנועות"));
            result.Add(new("UncompletedSteps", "שלבים שטרם הושלמו"));
            result.Add(new("StorageSystems", "מערכות"));

            return result;
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Process Random(string? id = null)
        {
            return new Process(
                id: id,
                definition: ProcessDefinition.Random(),
                events: Enumerable.Range(0, 3).Select(_ => Event.Random()).ToHashSet(),
                uncompletedSteps: Enumerable.Range(0, 3).Select(_ => ProcessStepDefinition.Random()).ToHashSet()
                );
        }

        public string Duration()
        {
            TimeSpan difference = End - Start;
            int days = difference.Days;
            int hours = difference.Hours;

            return $"{days} ימים, {hours} שעות";
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Fetch all processes which contain the searched text
        /// </summary>
        public static Tuple<List<Process>?, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Process>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
        new BooleanCondition(attribute: "Process.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Process.Definition.Name", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Process.Events.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: "Process.UncompletedSteps.Name", attributeRelation: AttributeRelation.Contains, value: SearchText,  @operator:Operator.Or)
                                                }, @operator: Operator.Or));
        }
    }
}
