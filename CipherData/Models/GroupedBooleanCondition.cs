using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    public class GroupedBooleanCondition : Condition
    {
        /// <summary>
        /// Any of BooleanCondition / GroupedBooleadCondition
        /// </summary>
        public HashSet<Condition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean.
        /// </summary>
        public Operator Operator { get; set; } = Operator.And;

        /// <summary>
        /// Instanciation of GroupedBooleanCondition
        /// </summary>
        /// <param name="conditions">Any of BooleanCondition / GroupedBooleadCondition</param>
        /// <param name="operator">Operator used to resolve the multiple condition results to a single boolean.</param>
        public GroupedBooleanCondition(HashSet<Condition> conditions, Operator @operator = Operator.And)
        {
            Conditions = conditions;
            Operator = @operator;
        }

        public static GroupedBooleanCondition Random()
        {
            return new GroupedBooleanCondition(
                conditions: new HashSet<Condition>()
                );
        }
    }
}
