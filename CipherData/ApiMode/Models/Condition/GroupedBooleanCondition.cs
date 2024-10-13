namespace CipherData.ApiMode
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    [HebrewTranslation(nameof(GroupedBooleanCondition))]
    public class GroupedBooleanCondition : Condition, IGroupedBooleanCondition
    {
        public IEnumerable<ICondition> Conditions { get; set; } = new List<Condition>();

        public Operator Operator { get; set; } = Operator.All;
    }
}
