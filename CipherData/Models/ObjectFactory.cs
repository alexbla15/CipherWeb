using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public enum Order
    {
        asc, desc
    }

    public enum Method
    {
        sum, avg, count, first, last
    }

    public class OrderedItem
    {
        /// <summary>
        /// Attribute to order by
        /// </summary>
        public string Attribute { get; set; }

        /// <summary>
        /// Desired order on the attribute
        /// </summary>
        public Order Order { get; set; }
    }

    public class AggregateItem
    {
        /// <summary>
        /// Attribute path to aggregate on
        /// </summary>
        public string Attribute { get; set; } = string.Empty;

        /// <summary>
        /// New name to give to the aggregated field. 
        /// if null, name is auto generated
        /// </summary>
        public string? As { get; set; }

        /// <summary>
        /// Method to aggregate the field by
        /// </summary>
        public Method Method { get; set; }
    }

    /// <summary>
    /// Method to get desired objects by filtering and aggregating the database
    /// </summary>
    public class ObjectFactory
    {
        /// <summary>
        /// Conditions to apply to get the desired objects. 
        /// All conditions must have the same target object.
        /// </summary>
        public GroupedBooleanCondition Filter { get; set; }

        /// <summary>
        /// Define order to the filtered objects
        /// </summary>
        public List<OrderedItem> OrderBy { get; set; }

        /// <summary>
        ///  List of object attributes to group by. 
        ///  If null, aggregates all the objects to a single one.
        /// </summary>
        public List<string>? GroupBy { get; set; }

        /// <summary>
        ///  List of aggregate methods defining the new object.
        ///  by default returns the grouped by fields if they 
        ///  exist. If null, returns the filtered objects
        /// </summary>
        public List<AggregateItem>? Aggregate { get; set; }
    }
}
