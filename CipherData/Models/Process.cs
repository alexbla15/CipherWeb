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

        public HashSet<Event> Events { get; set; }

        /// <summary>
        /// Uncompleted steps for completing the process
        /// </summary>
        public HashSet<ProcessStepDefinition> UncompletedSteps { get; set; }

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

        /// <summary>
        /// An instance of a specific processes
        /// </summary>
        /// <param name="definition">a collection of steps that make a single definition</param>
        /// <param name="events"></param>
        /// <param name="uncompletedSteps"></param>
        public Process(ProcessDefinition definition, HashSet<Event> events, HashSet<ProcessStepDefinition> uncompletedSteps,
            string? id = null)
        {
            Id = id ?? GetId();
            Definition = definition;
            Events = events;
            UncompletedSteps = uncompletedSteps;
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

        public static Process Random(string? id = null)
        {
            return new Process(
                id: id,
                definition: ProcessDefinition.Random(),
                events: new HashSet<Event>(),
                uncompletedSteps: Enumerable.Range(0, 3).Select(_ => ProcessStepDefinition.Random()).ToHashSet()
                );
        }
    }
}
