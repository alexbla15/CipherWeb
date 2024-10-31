namespace CipherData.RandomMode
{
    public class RandomCustomObjectBooleanCondition : BaseCustomObjectBooleanCondition, ICustomObjectBooleanCondition
    {
        public RandomCustomObjectBooleanCondition() 
        {
            Conditions = new();
            Operator = Operator.All;
        }
    }
}
