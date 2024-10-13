namespace CipherData.RandomMode
{
    public class RandomCustomObjectBooleanCondition : CipherClass, ICustomObjectBooleanCondition
    {
        /// <summary>
        /// List of object factory specifications and conditions on them
        /// </summary>
        public List<ICustomCondition> Conditions { get; set; } = new();

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean
        /// </summary>
        public Operator Operator { get; set; } = Operator.All;
    }
}
