namespace CipherData.ApiMode
{
    public abstract class Condition : CipherClass, ICondition { }

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

        public AttributeRelation? AttributeRelation { get; set; }

        public Operator? Operator { get; set; }
    }
}
