﻿namespace CipherData.RandomMode
{
    [HebrewTranslation(nameof(ProcessDefinition))]
    public class RandomProcessDefinition : Resource, IProcessDefinition
    {
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public new string? Id { get; set; } = GetNextId();

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string? Name { get; set; } = RandomFuncs.RandomItem(ProcessesNames);

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        public string? Description { get; set; } = RandomFuncs.RandomItem(ProcessesNames);

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Steps))]
        public List<IProcessStepDefinition> Steps { get; set; } = new() { new RandomProcessStepDefinition() };

        /// <summary>
        /// For randomization only
        /// </summary>
        public static readonly List<string> ProcessesNames = new() { "יצירה", "דגימה", "שינוי", "עיצוב" };

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        public static string GetNextId() => $"PD{++IdCounter:D3}";
    }
}