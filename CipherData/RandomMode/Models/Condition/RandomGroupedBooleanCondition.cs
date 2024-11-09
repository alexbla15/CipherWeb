namespace CipherData.RandomMode
{
    public class RandomGroupedBooleanCondition : BaseGroupedBooleanCondition, IGroupedBooleanCondition
    {
        public RandomGroupedBooleanCondition() 
        {
            Conditions = new List<Condition>() { 
                new BooleanCondition() { 
                Attribute=$"[{nameof(IProcessStepDefinition)}].[{nameof(IProcessStepDefinition.Description)}]", 
                    AttributeRelation=AttributeRelation.Ne, 
                    Operator=Operator.Any, 
                    Value=new Random().Next(100).ToString()
                } };
            Operator = Operator.All;
        }
    }
}
