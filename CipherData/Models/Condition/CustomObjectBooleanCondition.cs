namespace CipherData.Models
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
    [HebrewTranslation(nameof(CustomObjectBooleanCondition))]
    public class CustomObjectBooleanCondition : ICustomObjectBooleanCondition
    {
        [HebrewTranslation(typeof(CustomObjectBooleanCondition), nameof(Conditions))]
        public List<ICustomCondition> Conditions { get; set; } = new();

        [HebrewTranslation(typeof(CustomObjectBooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.All;
    }
}
