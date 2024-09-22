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
        /// Greater Than or equal
        /// </summary>
        Ge,
        /// <summary>
        /// Less than
        /// </summary>
        Lt,
        /// <summary>
        /// Less than or equal
        /// </summary>
        Le,
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
        IsNull,
        IsNotNull,
        IsEmpty,
        IsNotEmpty,
    }

    public enum Operator
    {
        /// <summary>
        /// All of the items in the array must follow the condition.
        /// </summary>
        All,
        /// <summary>
        /// At least one of the items in the array must follow the condition.
        /// </summary>
        Any
    }

    /// <summary>
    /// Abstract class for union of BooleanCondition and GroupedBooleanCondition
    /// </summary>
    public abstract class Condition : CipherClass
    {
    }

    /// <summary>
    /// Condition function on a single object
    /// </summary>
    public class BooleanCondition : Condition
    {
        private string? _Attribute = string.Empty;

        /// <summary>
        /// Attribute's name in event's object. 
        /// Can be chained to include sub-objects.
        /// example: obj.eventType, obj.system.id, obj.packages.category
        /// </summary>
        [HebrewTranslation(typeof(BooleanCondition), nameof(Attribute))]
        public string? Attribute { 
            get { return _Attribute; }
            set { _Attribute = value?.Trim(); }
        }

        /// <summary>
        /// Expected relation between attribute and a value.
        /// </summary>
        [HebrewTranslation(typeof(BooleanCondition), nameof(AttributeRelation))]
        public AttributeRelation AttributeRelation { get; set; } = AttributeRelation.Contains;

        /// <summary>
        /// Operator used in case the attribute contains multiple values.
        /// </summary>
        [HebrewTranslation(typeof(BooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.All;

        private string? _Value = null;

        /// <summary>
        /// Target value for comparision. 
        /// If null, the attributes are compared to 
        /// themselves (all equal, any equal etc.)
        /// </summary>
        [HebrewTranslation(typeof(BooleanCondition), nameof(Value))]
        public string? Value {
            get { return _Value; }
            set { _Value = value?.Trim(); }
        }

        /// <summary>
        /// Checks if this and other object are identical
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Equals(BooleanCondition? OtherObject)
        {
            if (OtherObject is null) return false;

            if (Attribute != OtherObject?.Attribute) return false;
            if (AttributeRelation != OtherObject?.AttributeRelation) return false;
            if (Operator != OtherObject?.Operator) return false;
            if (Value != OtherObject?.Value) return false;

            return true;
        }

        /// <summary>
        /// Create an identical object to this one.
        /// </summary>
        /// <returns></returns>
        public BooleanCondition Copy()
        {
            return (BooleanCondition)MemberwiseClone();
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAttribute()
        {
            return CheckField.Required(Attribute, Translate(nameof(Attribute)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckValue()
        {
            return (string.IsNullOrEmpty(Value)) ? new CheckField() :CheckField.Required(Value, Translate(nameof(Value)));
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckAttribute());
            result.Fields.Add(CheckValue());

            return result.Check();
        }
    }
}
