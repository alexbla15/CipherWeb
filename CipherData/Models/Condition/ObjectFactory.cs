namespace CipherData.Models
{
    public enum Order
    {
        asc, desc
    }

    public enum Method
    {
        sum, avg, count, first, last, max, min
    }

    [HebrewTranslation(nameof(OrderedItem))]
    public class OrderedItem : CipherClass
    {
        private string _Attribute = string.Empty;

        /// <summary>
        /// Attribute to order by
        /// </summary>
        [HebrewTranslation(typeof(OrderedItem), nameof(Attribute))]
        public string Attribute { 
            get { return _Attribute; }
            set { _Attribute = value.Trim(); }
        }

        /// <summary>
        /// Desired order on the attribute
        /// </summary>
        [HebrewTranslation(typeof(OrderedItem), nameof(Order))]
        public Order Order { get; set; } = Order.asc;
    }

    [HebrewTranslation(nameof(AggregateItem))]
    public class AggregateItem : CipherClass
    {
        private string? _Attribute = null;
        private string? _As = null;

        /// <summary>
        /// Attribute path to aggregate on
        /// </summary>
        [HebrewTranslation(typeof(AggregateItem), nameof(Attribute))]
        public string? Attribute {
            get => _Attribute; 
            set => _Attribute = value?.Trim();
        }

        /// <summary>
        /// New name to give to the aggregated field. 
        /// if null, name is auto generated
        /// </summary>
        [HebrewTranslation(typeof(AggregateItem), nameof(As))]
        public string? As
        {
            get => _As; 
            set => _As = value?.Trim();
        }

        /// <summary>
        /// Method to aggregate the field by
        /// </summary>
        [HebrewTranslation(typeof(AggregateItem), nameof(Method))]
        public Method? Method { get; set; }
    }

    /// <summary>
    /// Method to get desired objects by filtering and aggregating the database
    /// </summary>
    [HebrewTranslation(nameof(ObjectFactory))]
    public class ObjectFactory : CipherClass
    {
        /// <summary>
        /// Conditions to apply to get the desired objects. 
        /// All conditions must have the same target object.
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(Attribute))]
        public GroupedBooleanCondition Filter { get; set; } = new();

        /// <summary>
        /// Define order to the filtered objects
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(OrderBy))]
        public List<OrderedItem>? OrderBy { get; set; }

        /// <summary>
        ///  List of object attributes to group by. 
        ///  If null, aggregates all the objects to a single one.
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(GroupBy))]
        public List<string>? GroupBy { get; set; } = new();

        /// <summary>
        ///  List of aggregate methods defining the new object.
        ///  by default returns the grouped by fields if they 
        ///  exist. If null, returns the filtered objects
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(Aggregate))]
        public List<AggregateItem>? Aggregate { get; set; }

        public void AddOrderBy(OrderedItem nextOrder)
        {
            OrderBy ??= new List<OrderedItem>();
            OrderBy.Add(nextOrder);
        }
    }
}
