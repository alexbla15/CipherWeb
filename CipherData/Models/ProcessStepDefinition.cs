﻿using CipherData.Requests;
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
    public class ProcessStepDefinition: Resource
    {
        /// <summary>
        /// Unique name of the process step, two steps in the same process should not have the same name.
        /// </summary>
        [HebrewTranslation("שם")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the process step
        /// </summary>
        [HebrewTranslation("תיאור")]
        public string Description { get; set; }

        /// <summary>
        /// Condition on event to be associated with the process step 
        /// </summary>
        [HebrewTranslation("תנאי")]
        public GroupedBooleanCondition Condition { get; set; }

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

        /// <summary>
        /// A collection of steps that make a single definition
        /// </summary>
        /// <param name="name">Unique name of the process step, two steps in the same process should not have the same name.</param>
        /// <param name="description">Description of the process step</param>
        /// <param name="condition">Condition on event to be associated with the process step </param>
        public ProcessStepDefinition(string name, string description, GroupedBooleanCondition condition, string? id = null)
        {
            Id = id ?? GetId();
            Name = name;
            Description = description;
            Condition = condition;
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<ProcessStepDefinition>());

            return result.ToHashSet();
        }

        public static ProcessStepDefinition Random(string? id = null)
        {
            List<string> ProcessesStepNames = new() { "רישום", "עדכון במערכת", "השהייה" };
            string name = TestedData.RandomString(ProcessesStepNames);

            return new ProcessStepDefinition(
                id: id,
                name: name,
                description:name,
                condition: GroupedBooleanCondition.Random()
                );
        }
    }
}
