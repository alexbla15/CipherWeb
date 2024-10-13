using System.Reflection;

namespace CipherData.Interfaces
{
    public interface IProcessStepDefinition : IResource
    {
        /// <summary>
        /// Condition on event to be associated with the process step 
        /// </summary>
        IGroupedBooleanCondition Condition { get; set; }

        /// <summary>
        /// Description of process
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Name of the process
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckName() => CheckField.Required(Name, Translate(nameof(Name)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDescription() => CheckField.Required(Description, Translate(nameof(Description)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckCondition()
        {
            Tuple<bool, string> result = Condition.Check();
            return new CheckField(result.Item1, result.Item2);
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckName());
            result.Fields.Add(CheckDescription());
            result.Fields.Add(CheckCondition());

            return result.Check();
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
