using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    [HebrewTranslation(nameof(IGroupedBooleanCondition))]
    public interface IGroupedBooleanCondition : ICondition
    {
        /// <summary>
        /// Any of BooleanCondition / GroupedBooleadCondition
        /// </summary>
        [HebrewTranslation(typeof(IGroupedBooleanCondition), nameof(Conditions))]
        IEnumerable<ICondition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean.
        /// </summary>
        [HebrewTranslation(typeof(IGroupedBooleanCondition), nameof(Operator))]
        Operator Operator { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConditions()
        {
            foreach (var cond in Conditions)
            {
                var result = cond switch
                {
                    IBooleanCondition singleCond => singleCond.Check() ?? Tuple.Create(false, "שגיאת מערכת"),
                    IGroupedBooleanCondition groupCond => groupCond.Check() ?? Tuple.Create(false, "שגיאת מערכת"),
                    _ => Tuple.Create(true, string.Empty)
                };

                if (!result.Item1) return new CheckField(result.Item1, result.Item2);
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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    public abstract class BaseGroupedBooleanCondition : CipherClass, IGroupedBooleanCondition
    {
        public IEnumerable<ICondition> Conditions { get; set; } = new List<Condition>();

        public Operator Operator { get; set; } = Operator.All;
    }
}
