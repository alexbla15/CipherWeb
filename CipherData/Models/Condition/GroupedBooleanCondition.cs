namespace CipherData.Models
{
    public interface IGroupedBooleanCondition
    {
        /// <summary>
        /// Any of BooleanCondition / GroupedBooleadCondition
        /// </summary>
        IEnumerable<Condition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean.
        /// </summary>
        Operator Operator { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConditions();

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check();
    }

    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    [HebrewTranslation(nameof(GroupedBooleanCondition))]
    public class GroupedBooleanCondition : Condition, IGroupedBooleanCondition
    {
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Conditions))]
        public IEnumerable<Condition> Conditions { get; set; } = new List<Condition>();

        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.All;

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

        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckConditions());
            return result.Check();
        }
    }
}
