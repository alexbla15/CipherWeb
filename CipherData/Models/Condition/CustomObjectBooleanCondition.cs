namespace CipherData.Models
{
    public class CustomCondition
    {
        [HebrewTranslation(typeof(CustomCondition), nameof(Factory))]
        public ObjectFactory Factory { get; set; }

        [HebrewTranslation(typeof(CustomCondition), nameof(ObjectCondition))]
        public GroupedBooleanCondition ObjectCondition { get; set; }
    }

    /// <summary>
    /// Complex boolean condition that is applied to custom 
    /// objects created from an object factory
    /// </summary>
    public class CustomObjectBooleanCondition
    {
        /// <summary>
        /// List of object factory specifications and conditions on them
        /// </summary>
        [HebrewTranslation(typeof(CustomObjectBooleanCondition), nameof(Conditions))]
        public List<CustomCondition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean
        /// </summary>
        [HebrewTranslation(typeof(CustomObjectBooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.And;

        /// <summary>
        /// Complex boolean condition that is applied to custom 
        /// objects created from an object factory
        /// </summary>
        /// <param name="conditions">List of object factory specifications and conditions on them</param>
        /// <param name="operator"></param>
        public CustomObjectBooleanCondition(List<CustomCondition> conditions, Operator @operator = Operator.And)
        {
            Conditions = conditions;
            Operator = @operator;
        }

        /// <summary>
        /// Create a random object.
        /// </summary>
        public static CustomObjectBooleanCondition Random()
        {
            return new CustomObjectBooleanCondition(conditions: new List<CustomCondition>());
        }
    }
}
