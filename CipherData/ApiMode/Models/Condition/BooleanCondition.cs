namespace CipherData.ApiMode
{
    /// <summary>
    /// Abstract class for union of BooleanCondition and GroupedBooleanCondition
    /// </summary>
    public abstract class Condition : CipherClass, ICondition { }

    /// <summary>
    /// Condition function on a single object
    /// </summary>
    [HebrewTranslation(nameof(BooleanCondition))]
    public class BooleanCondition : Condition, IBooleanCondition
    {
        private string? _Attribute = string.Empty;
        private string? _Value = null;

        [HebrewTranslation(typeof(BooleanCondition), nameof(Attribute))]
        public string? Attribute
        {
            get => _Attribute;
            set => _Attribute = value?.Trim();
        }

        [HebrewTranslation(typeof(BooleanCondition), nameof(Value))]
        public string? Value { get => _Value; set => _Value = value?.Trim(); }

        [HebrewTranslation(typeof(BooleanCondition), nameof(AttributeRelation))]
        public AttributeRelation AttributeRelation { get; set; } = AttributeRelation.Eq;

        [HebrewTranslation(typeof(BooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; }
    }
}
