using System.Reflection;

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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
