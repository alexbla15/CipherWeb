namespace CipherData.ApiMode
{
    public class OrderedItem : CipherClass, IOrderedItem
    {
        private string _Attribute = string.Empty;

        public string Attribute { get => _Attribute; set => _Attribute = value.Trim(); }

        public Order Order { get; set; } = Order.asc;
    }

    public class AggregateItem : CipherClass, IAggregateItem
    {
        private string? _Attribute = null;
        private string? _As = null;

        public string? Attribute { get => _Attribute; set => _Attribute = value?.Trim(); }

        public string? As
        {
            get => _As;
            set => _As = value?.Trim();
        }

        public Method? Method { get; set; }
    }

    /// <summary>
    /// Method to get desired objects by filtering and aggregating the database
    /// </summary>
    public class ObjectFactory : CipherClass, IObjectFactory
    {
        public IGroupedBooleanCondition Filter { get; set; } = new GroupedBooleanCondition();

        public List<IOrderedItem>? OrderBy { get; set; }

        public List<string>? GroupBy { get; set; } = new();

        public List<IAggregateItem>? Aggregate { get; set; }
    }
}
