namespace CipherData.RandomMode
{
    public class RandomGroupedBooleanCondition : BaseGroupedBooleanCondition, IGroupedBooleanCondition
    {
        public RandomGroupedBooleanCondition() 
        {
            Conditions = new List<Condition>();
            Operator = Operator.All;
        }
    }
}
