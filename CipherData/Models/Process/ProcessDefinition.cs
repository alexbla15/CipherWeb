﻿using CipherData.Randomizer;

namespace CipherData.Models
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    [HebrewTranslation(nameof(ProcessDefinition))]
    public class ProcessDefinition: Resource
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string? Name {
            get => _Name; 
            set => _Name = value?.Trim(); 
        }

        /// <summary>
        /// Description of process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
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
        public ProcessDefinition(string? id = null) => Id = id ?? GetNextId();

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        public static string GetNextId() => $"PD{++IdCounter:D3}";

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

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<ProcessDefinition>, ErrorResponse> All() => Config.ProcessesDefinitionsRequests.GetProcessDefinitions();

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
