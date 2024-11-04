namespace CipherData.RandomMode
{
    public class RandomGroupedBooleanCondition : BaseGroupedBooleanCondition, IGroupedBooleanCondition
    {
        public RandomGroupedBooleanCondition() 
        {
            Conditions = new List<Condition>() { new BooleanCondition() { Attribute="a", AttributeRelation=AttributeRelation.Ne, 
                Operator=Operator.Any, Value="44"} };
            Operator = Operator.All;
        }
    }
}
