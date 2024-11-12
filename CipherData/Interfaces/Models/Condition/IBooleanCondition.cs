using System.Reflection;

namespace CipherData.Interfaces
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

        // NOW FOR RELATIONS THAT MUST HAVE A PARAMETER

        EqParam,
        NeParam,
        GtParam,
        GeParam,
        LtParam,
        LeParam,
        StartsWithParam,
        EndsWithParam,
        ContainsParam,
        NotContainsParam
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
    public interface ICondition : ICipherClass { }

    /// <summary>
    /// Condition function on a single object
    /// </summary>
    [HebrewTranslation(nameof(IBooleanCondition))]
    public interface IBooleanCondition : ICondition
    {
        /// <summary>
        /// Attribute's name in event's object. 
        /// Can be chained to include sub-objects.
        /// example: obj.eventType, obj.system.id, obj.packages.category
        /// </summary>
        [HebrewTranslation(typeof(IBooleanCondition), nameof(Attribute))]
        [Check(CheckRequirement.Required, allowedRegex: @"^[a-zA-Z0-9א-ת.,\]\[ \n?]+$")]
        string? Attribute { get; set; }

        /// <summary>
        /// Target value for comparison. 
        /// If null, the attributes are compared to 
        /// themselves (all equal, any equal etc.)
        /// </summary>
        [HebrewTranslation(typeof(IBooleanCondition), nameof(Value))]
        string? Value { get; set; }

        /// <summary>
        /// Expected relation between attribute and a value.
        /// </summary>
        [HebrewTranslation(typeof(IBooleanCondition), nameof(AttributeRelation))]
        AttributeRelation? AttributeRelation { get; set; }

        /// <summary>
        /// Operator used in case the attribute contains multiple values.
        /// </summary>
        [HebrewTranslation(typeof(IBooleanCondition), nameof(Operator))]
        [Check(CheckRequirement.Required)]
        Operator? Operator { get; set; }

        public CheckField CheckOperator() => CheckProperty(this, nameof(Operator));

        public CheckField CheckAttribute() => CheckProperty(this, nameof(Attribute));

        public CheckField CheckAttributeRelation() =>
            CheckField.Required(AttributeRelation, $"{Translate(nameof(AttributeRelation))} עבור {CipherField.TranslatePath(Attribute)}");

        public CheckField CheckValue(bool allRegex = false) => 
            allRegex? new() : CheckField.CheckString(Value, CipherField.TranslatePath(Attribute) ?? Translate(nameof(Value)),
                @"^[a-zA-Z0-9א-ת.:,\]\[ \n?]+$");

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check(bool allRegex = false)
        {
            CheckClass result = new()
            {
                Fields = new()
                {
                    CheckAttributeRelation(), CheckValue(allRegex), CheckOperator(),
                    CheckAttribute()
                }
            };

            return result.Check();
        }

        /// <summary>
        /// Method to export the object to an api-appropriate form
        /// 1 - exchange AttributeRelation only to 
        /// </summary>
        public IBooleanCondition Export()
        {
            Dictionary<AttributeRelation, AttributeRelation> ParameterToRegularRelation = new()
            {
                [Interfaces.AttributeRelation.EqParam] = Interfaces.AttributeRelation.Eq,
                [Interfaces.AttributeRelation.NeParam] = Interfaces.AttributeRelation.Ne,
                [Interfaces.AttributeRelation.LeParam] = Interfaces.AttributeRelation.Le,
                [Interfaces.AttributeRelation.LtParam] = Interfaces.AttributeRelation.Lt,
                [Interfaces.AttributeRelation.GeParam] = Interfaces.AttributeRelation.Ge,
                [Interfaces.AttributeRelation.GtParam] = Interfaces.AttributeRelation.Gt,
                [Interfaces.AttributeRelation.EndsWithParam] = Interfaces.AttributeRelation.EndsWith,
                [Interfaces.AttributeRelation.StartsWithParam] = Interfaces.AttributeRelation.StartsWith,
                [Interfaces.AttributeRelation.NotContainsParam] = Interfaces.AttributeRelation.NotContains,
                [Interfaces.AttributeRelation.ContainsParam] = Interfaces.AttributeRelation.Contains,
            };

            IBooleanCondition copyItem = Config.BooleanCondition();

            copyItem.Attribute = Attribute is null ? null : 
                $"[{string.Join("].[", Attribute.Trim('[',']').Split("].[").Select(x=>x.Trim('I')))}]";

            copyItem.AttributeRelation = AttributeRelation is null ?
                null : (ParameterToRegularRelation.ContainsKey((AttributeRelation)AttributeRelation) ?
                ParameterToRegularRelation[(AttributeRelation)AttributeRelation] : AttributeRelation);
            copyItem.Operator = Operator;
            copyItem.Value = Value;

            return copyItem;
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    public abstract class BaseBooleanCondition: Condition, IBooleanCondition
    {
        public string? Attribute { get; set; }
        public string? Value { get; set; }
        public AttributeRelation? AttributeRelation { get; set; }
        public Operator? Operator { get; set; }
    }
}
