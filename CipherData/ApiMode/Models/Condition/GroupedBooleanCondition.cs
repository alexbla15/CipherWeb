namespace CipherData.ApiMode
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    [HebrewTranslation(nameof(GroupedBooleanCondition))]
    public class GroupedBooleanCondition : Condition, IGroupedBooleanCondition
    {
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Conditions))]
        public IEnumerable<ICondition> Conditions { get; set; } = new List<Condition>();

        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.All;
    }
}
