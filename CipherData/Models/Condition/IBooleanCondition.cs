namespace CipherData.Models
{
    public interface ICondition { }

    public interface IBooleanCondition : ICondition
    {
        /// <summary>
        /// Attribute's name in event's object. 
        /// Can be chained to include sub-objects.
        /// example: obj.eventType, obj.system.id, obj.packages.category
        /// </summary>
        string? Attribute { get; set; }

        /// <summary>
        /// Target value for comparision. 
        /// If null, the attributes are compared to 
        /// themselves (all equal, any equal etc.)
        /// </summary>
        string? Value { get; set; }

        /// <summary>
        /// Expected relation between attribute and a value.
        /// </summary>
        AttributeRelation AttributeRelation { get; set; }

        /// <summary>
        /// Operator used in case the attribute contains multiple values.
        /// </summary>
        Operator Operator { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAttribute() => CheckField.Required(Attribute, BooleanCondition.Translate(nameof(Attribute)), @"^[a-zA-Z0-9א-ת.,\]\[ \n?]+$");

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckValue() => CheckField.CheckString(Value, BooleanCondition.Translate(nameof(Value)));

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckAttribute());
            result.Fields.Add(CheckValue());

            return result.Check();
        }
    }
}
