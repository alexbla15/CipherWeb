namespace CipherData.Models
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    [HebrewTranslation(nameof(GroupedBooleanCondition))]
    public class GroupedBooleanCondition : Condition
    {
        /// <summary>
        /// Any of BooleanCondition / GroupedBooleadCondition
        /// </summary>
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Conditions))]
        public IEnumerable<Condition> Conditions { get; set; } = new List<Condition>();

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean.
        /// </summary>
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.All;

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConditions()
        {
            if (Conditions.Any())
            {
                foreach (var cond in Conditions)
                {
                    Tuple<bool, string> result = Tuple.Create(true, string.Empty);
                    if (cond is BooleanCondition)
                    {
                        result = (cond as BooleanCondition).Check();
                    }
                    else if (cond is GroupedBooleanCondition)
                    {
                        result = (cond as GroupedBooleanCondition).Check();
                    }
                    return new CheckField(result.Item1, result.Item2);
                }
            }
            return new CheckField();

        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckConditions());
            return result.Check();
        }

        /// <summary>
        /// Create a random object.
        /// </summary>
        public static GroupedBooleanCondition Random() => new();
    }
}
