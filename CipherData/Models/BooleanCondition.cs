using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public enum AttributeRelation
    {
        Eq, // equal
        Ne, // not equal
        Gt, // greater than
        Lt // less than
    }

    public enum Operator
    {
        All,
        And, 
        Any, 
        Or
    }

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
        public string Attribute { get; set; }

        /// <summary>
        /// Expected relation between attribute and a value.
        /// </summary>
        public AttributeRelation AttributeRelation { get; set; }

        /// <summary>
        /// Operator used in case the attribute contains multiple values.
        /// </summary>
        public Operator Operator { get; set; } = Operator.And;

        /// <summary>
        /// Target value for comparision. 
        /// If null, the attributes are compared to 
        /// themselves (all equal, any equal etc.)
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// User's instanciation of a boolean condition.
        /// </summary>
        /// <param name="attribute">Attribute's name in event's object. Can be chained to include sub-objects. Example: obj.eventType, obj.system.id, obj.packages.category</param>
        /// <param name="attributeRelation">Expected relation between attribute and a value.</param>
        /// <param name="operator">Operator used in case the attribute contains multiple values.</param>
        /// <param name="value">Target value for comparision. If null, the attributes are compared to themselves (all equal, any equal etc.)</param>
        public BooleanCondition(
            string attribute,
            AttributeRelation attributeRelation, 
            Operator @operator = Operator.And, 
            string? value = null)
        {
            Attribute = attribute;
            AttributeRelation = attributeRelation;
            Operator = @operator;
            Value = value;
        }
    }
}
