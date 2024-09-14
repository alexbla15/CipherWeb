﻿namespace CipherData.Models
{
    /// <summary>
    /// Create a new process definition or update it.
    /// </summary>
    public class ProcessDefinitionRequest
    {
        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(ProcessDefinition.Name))]
        public string Name { get; set; }

        /// <summary>
        /// Description of process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(ProcessDefinition.Description))]
        public string Description { get; set; }

        /// <summary>
        /// Steps of the process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(ProcessDefinition.Steps))]
        public List<ProcessStepDefinition> Steps { get; set; }

        /// <summary>
        /// Create a new process definition or update it.
        /// </summary>
        /// <param name="name">Name of the process</param>
        /// <param name="description">Description of process</param>
        /// <param name="steps">Steps of the process</param>
        public ProcessDefinitionRequest(string name, string description, List<ProcessStepDefinition> steps)
        {
            Name = name;
            Description = description;
            Steps = steps;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Name)) ? result : Tuple.Create(false, ProcessDefinition.Translate(nameof(RandomData.RandomProcessDefinition.Name))); // required
            result = (!string.IsNullOrEmpty(Description)) ? result : Tuple.Create(false, ProcessDefinition.Translate(nameof(RandomData.RandomProcessDefinition.Description))); // required
            result = (Steps.Count > 0) ? result : Tuple.Create(false, ProcessDefinition.Translate(nameof(RandomData.RandomProcessDefinition.Steps))); // required

            return result;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>s
        public string ToJson()
        {
            return Resource.ToJson(this);
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
                        different |= step.Compare(OtherObject.Steps.Where(x => x.Name == step.Name).First());
                    }
                }
            }
            else
            {
                different = true;
            }

            return different;
        }

        /// <summary>
        /// Get an empty object scheme.
        /// </summary>
        public static ProcessDefinitionRequest Empty()
        {
            return new ProcessDefinitionRequest(name: string.Empty, description: string.Empty, steps: new List<ProcessStepDefinition>());
        }
    }
}
