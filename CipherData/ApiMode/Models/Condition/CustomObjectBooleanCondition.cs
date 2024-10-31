namespace CipherData.ApiMode
{
    public class CustomCondition : ICustomCondition
    {
        public IObjectFactory? Factory { get; set; }

        public IGroupedBooleanCondition? ObjectCondition { get; set; }
    }

    public class CustomObjectBooleanCondition : BaseCustomObjectBooleanCondition, ICustomObjectBooleanCondition
    {
    }
}
