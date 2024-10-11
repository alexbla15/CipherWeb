namespace CipherData.Models
{
    public interface ICustomCondition
    {
        IObjectFactory? Factory { get; set; }
        IGroupedBooleanCondition? ObjectCondition { get; set; }
    }

    public interface ICustomObjectBooleanCondition
    {
        /// <summary>
        /// List of object factory specifications and conditions on them
        /// </summary>
        List<ICustomCondition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean
        /// </summary>
        Operator Operator { get; set; }
    }
}
