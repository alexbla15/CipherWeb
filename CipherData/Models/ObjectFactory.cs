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

        /// <summary>
        /// Instanciation of OrderedItem
        /// </summary>
        /// <param name="attribute">Attribute to order by</param>
        /// <param name="order">Desired order on the attribute</param>
        public OrderedItem (string attribute, Order order = Order.asc)
        {
            Attribute = attribute;
            Order = order;
        }
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

        /// <summary>
        /// Instanciation of AggregateItem
        /// </summary>
        /// <param name="attribute">Attribute path to aggregate on</param>
        /// <param name="method">Method to aggregate the field by</param>
        /// <param name="as">New name to give to the aggregated field. If null, name is auto generated</param>
        public AggregateItem(string attribute, Method method, string? @as = null)
        {
            Attribute = attribute;
            As = @as;
            Method = method;
        }
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
        public HashSet<OrderedItem>? OrderBy { get; set; }

        /// <summary>
        ///  List of object attributes to group by. 
        ///  If null, aggregates all the objects to a single one.
        /// </summary>
        public HashSet<string>? GroupBy { get; set; }

        /// <summary>
        ///  List of aggregate methods defining the new object.
        ///  by default returns the grouped by fields if they 
        ///  exist. If null, returns the filtered objects
        /// </summary>
        public HashSet<AggregateItem>? Aggregate { get; set; }

        /// <summary>
        /// Method to get desired objects by filtering and aggregating the database
        /// </summary>
        /// <param name="filter">Conditions to apply to get the desired objects. All conditions must have the same target object.</param>
        /// <param name="orderBy">Define order to the filtered objects</param>
        /// <param name="groupBy">List of object attributes to group by. If null, aggregates all the objects to a single one.</param>
        /// <param name="aggregate">List of aggregate methods defining the new object. by default returns the grouped by fields if they exist. If null, returns the filtered objects</param>
        public ObjectFactory(
            GroupedBooleanCondition filter, HashSet<OrderedItem>? orderBy = null, HashSet<string>? groupBy = null, HashSet<AggregateItem>? aggregate = null)
        {
            Filter = filter;
            OrderBy = orderBy;
            GroupBy = groupBy;
            Aggregate = aggregate;
        }
    }
}
