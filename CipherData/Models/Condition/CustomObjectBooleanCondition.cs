namespace CipherData.Models
{
    public interface ICustomCondition
    {
        ObjectFactory? Factory { get; set; }
        GroupedBooleanCondition? ObjectCondition { get; set; }
    }

    public class CustomCondition : ICustomCondition
    {
        [HebrewTranslation(typeof(CustomCondition), nameof(Factory))]
        public ObjectFactory? Factory { get; set; }

        [HebrewTranslation(typeof(CustomCondition), nameof(ObjectCondition))]
        public GroupedBooleanCondition? ObjectCondition { get; set; }
    }

    public interface ICustomObjectBooleanCondition
    {
        /// <summary>
        /// List of object factory specifications and conditions on them
        /// </summary>
        List<ICustomCondition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean
        /// </summary>
        Operator Operator { get; set; }
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
