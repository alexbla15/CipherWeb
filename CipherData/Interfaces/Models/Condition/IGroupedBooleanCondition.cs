using System.Reflection;
using System.Text.Json;

namespace CipherData.Interfaces
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    [HebrewTranslation(nameof(IGroupedBooleanCondition))]
    public interface IGroupedBooleanCondition : ICondition
    {
        /// <summary>
        /// Any of BooleanCondition / GroupedBooleanCondition
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
        public CheckField CheckConditions(bool allRegex = false)
        {
            foreach (var condition in Conditions)
            {
                var result = condition switch
                {
                    IBooleanCondition singleCond => singleCond.Check(allRegex) ?? Tuple.Create(false, "שגיאת מערכת"),
                    IGroupedBooleanCondition groupCond => groupCond.Check(allRegex) ?? Tuple.Create(false, "שגיאת מערכת"),
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
        public Tuple<bool, string> Check(bool allRegex= false)
        {
            CheckClass result = new();
            result.Fields.Add(CheckConditions(allRegex));
            return result.Check();
        }

        public IGroupedBooleanCondition Export()
        {
            IGroupedBooleanCondition copyItem = Config.GroupedBooleanCondition();
            copyItem.Conditions = Conditions.Select(x => (x is IBooleanCondition singleX) ?
            singleX.Export() : x).ToList();
            copyItem.Operator = Operator;
            return copyItem;
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    public abstract class BaseGroupedBooleanCondition : Condition, IGroupedBooleanCondition
    {
        public IEnumerable<ICondition> Conditions { get; set; } = new List<Condition>();

        public Operator Operator { get; set; } = Operator.All;
    }
}
