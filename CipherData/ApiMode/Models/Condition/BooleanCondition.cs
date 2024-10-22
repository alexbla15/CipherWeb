namespace CipherData.ApiMode
{
    /// <summary>
    /// Abstract class for union of BooleanCondition and GroupedBooleanCondition
    /// </summary>
    public abstract class Condition : CipherClass, ICondition { }

    /// <summary>
    /// Condition function on a single object
    /// </summary>
    public class BooleanCondition : Condition, IBooleanCondition
    {
        private string? _Attribute = string.Empty;
        private string? _Value = null;

        public string? Attribute
        {
            get => _Attribute;
            set => _Attribute = value?.Trim();
        }

        public string? Value { get => _Value; set => _Value = value?.Trim(); }

        public AttributeRelation AttributeRelation { get; set; } = AttributeRelation.Eq;

        public Operator Operator { get; set; }
    }
}
