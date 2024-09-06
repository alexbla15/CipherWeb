namespace CipherData.Models
{
    public enum AttributeRelation
    {
        /// <summary>
        /// Equal
        /// </summary>
        Eq,
        /// <summary>
        /// Not equal
        /// </summary>
        Ne,
        /// <summary>
        /// Greater Than
        /// </summary>
        Gt,
        /// <summary>
        /// Less than
        /// </summary>
        Lt,
        /// <summary>
        /// Contains text
        /// </summary>
        Contains
    }

    public enum Operator
    {
        /// <summary>
        /// All of the items in the array must follow the condition.
        /// </summary>
        All,
        /// <summary>
        /// 
        /// </summary>
        And, 
        /// <summary>
        /// At least one of the items in the array must follow the condition.
        /// </summary>
        Any,
        /// <summary>
        /// 
        /// </summary>
        Or
    }

    /// <summary>
    /// Abstract class for union of BooleanCondition and GroupedBooleanCondition
    /// </summary>
    public abstract class Condition
    {
    }

    /// <summary>
    /// Condition function on a single object
    /// </summary>
    public class BooleanCondition : Condition
    {
        /// <summary>
        /// Attribute's name in event's object. 
        /// Can be chained to include sub-objects.
        /// example: obj.eventType, obj.system.id, obj.packages.category
        /// </summary>
        [HebrewTranslation(Translator.Condition_Attribute)]
        public string Attribute { get; set; }

        /// <summary>
        /// Expected relation between attribute and a value.
        /// </summary>
        [HebrewTranslation(Translator.Condition_Relation)]
        public AttributeRelation AttributeRelation { get; set; }

        /// <summary>
        /// Operator used in case the attribute contains multiple values.
        /// </summary>
        [HebrewTranslation(Translator.Condition_Operator)]
        public Operator Operator { get; set; } = Operator.And;

        /// <summary>
        /// Target value for comparision. 
        /// If null, the attributes are compared to 
        /// themselves (all equal, any equal etc.)
        /// </summary>
        [HebrewTranslation(Translator.Condition_Value)]
        public string? Value { get; set; }

        /// <summary>
        /// User's instanciation of a boolean condition.
        /// </summary>
        /// <param name="attribute">Attribute's name in event's object. Can be chained to include sub-objects. Example: obj.eventType, obj.system.id, obj.packages.category</param>
        /// <param name="attributeRelation">Expected relation between attribute and a value.</param>
        /// <param name="operator">Operator used in case the attribute contains multiple values.</param>
        /// <param name="value">Target value for comparision. If null, the attributes are compared to themselves (all equal, any equal etc.)</param>
        public BooleanCondition(string attribute, AttributeRelation attributeRelation, Operator @operator = Operator.And, 
            string? value = null)
        {
            Attribute = attribute;
            AttributeRelation = attributeRelation;
            Operator = @operator;
            Value = value;
        }

        /// <summary>
        /// Get an empty object scheme.
        /// </summary>
        public static BooleanCondition Empty()
        {
            return new BooleanCondition(attribute: string.Empty, attributeRelation: AttributeRelation.Contains);
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(BooleanCondition), searchedAttribute);
        }
    }
}
