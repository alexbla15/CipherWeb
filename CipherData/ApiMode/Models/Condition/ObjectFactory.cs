namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(OrderedItem))]
    public class OrderedItem : CipherClass, IOrderedItem
    {
        private string _Attribute = string.Empty;

        [HebrewTranslation(typeof(OrderedItem), nameof(Attribute))]
        public string Attribute { get => _Attribute; set => _Attribute = value.Trim(); }

        [HebrewTranslation(typeof(OrderedItem), nameof(Order))]
        public Order Order { get; set; } = Order.asc;
    }

    [HebrewTranslation(nameof(AggregateItem))]
    public class AggregateItem : CipherClass, IAggregateItem
    {
        private string? _Attribute = null;
        private string? _As = null;

        [HebrewTranslation(typeof(AggregateItem), nameof(Attribute))]
        public string? Attribute { get => _Attribute; set => _Attribute = value?.Trim(); }

        [HebrewTranslation(typeof(AggregateItem), nameof(As))]
        public string? As
        {
            get => _As;
            set => _As = value?.Trim();
        }

        [HebrewTranslation(typeof(AggregateItem), nameof(Method))]
        public Method? Method { get; set; }
    }

    /// <summary>
    /// Method to get desired objects by filtering and aggregating the database
    /// </summary>
    [HebrewTranslation(nameof(ObjectFactory))]
    public class ObjectFactory : CipherClass, IObjectFactory
    {
        [HebrewTranslation(typeof(ObjectFactory), nameof(Attribute))]
        public IGroupedBooleanCondition Filter { get; set; } = new GroupedBooleanCondition();

        [HebrewTranslation(typeof(ObjectFactory), nameof(OrderBy))]
        public List<IOrderedItem>? OrderBy { get; set; }

        [HebrewTranslation(typeof(ObjectFactory), nameof(GroupBy))]
        public List<string>? GroupBy { get; set; } = new();

        [HebrewTranslation(typeof(ObjectFactory), nameof(Aggregate))]
        public List<IAggregateItem>? Aggregate { get; set; }
    }
}
