namespace CipherData.ApiMode
{
    public class CustomCondition : ICustomCondition
    {
        [HebrewTranslation(typeof(CustomCondition), nameof(Factory))]
        public IObjectFactory? Factory { get; set; }

        [HebrewTranslation(typeof(CustomCondition), nameof(ObjectCondition))]
        public IGroupedBooleanCondition? ObjectCondition { get; set; }
    }

    /// <summary>
    /// Complex boolean condition that is applied to custom 
    /// objects created from an object factory
    /// </summary>
    public class CustomObjectBooleanCondition : BaseCustomObjectBooleanCondition, ICustomObjectBooleanCondition
    {
    }
}
