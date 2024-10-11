namespace CipherData.Models.Randomizers
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    [HebrewTranslation(nameof(GroupedBooleanCondition))]
    public class RandomGroupedBooleanCondition : Condition, IGroupedBooleanCondition
    {
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Conditions))]
        public IEnumerable<ICondition> Conditions { get; set; } = new List<Condition>();

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
                        result = (cond as IBooleanCondition).Check();
                    }
                    else if (cond is GroupedBooleanCondition)
                    {
                        result = (cond as IGroupedBooleanCondition).Check();
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
