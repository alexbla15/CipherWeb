using System.Reflection;

namespace CipherData.Interfaces
{
    public enum Order
    {
        asc, desc
    }

    public enum Method
    {
        sum, avg, count, first, last, max, min
    }

    public interface IOrderedItem : ICipherClass
    {
        /// <summary>
        /// Attribute to order by
        /// </summary>
        [HebrewTranslation(typeof(OrderedItem), nameof(Attribute))]
        string Attribute { get; set; }

        /// <summary>
        /// Desired order on the attribute
        /// </summary>
        [HebrewTranslation(typeof(OrderedItem), nameof(Order))]
        Order Order { get; set; }

        public CheckField CheckAttribute() => CheckField.Required(Attribute, Translate(nameof(Attribute)));

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckAttribute());

            return result.Check();
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    public interface IAggregateItem : ICipherClass
    {
        /// <summary>
        /// New name to give to the aggregated field. 
        /// if null, name is auto generated
        /// </summary>
        [HebrewTranslation(typeof(AggregateItem), nameof(As))]
        string? As { get; set; }

        /// <summary>
        /// Attribute path to aggregate on
        /// </summary>
        [HebrewTranslation(typeof(AggregateItem), nameof(Attribute))]
        string? Attribute { get; set; }

        /// <summary>
        /// Method to aggregate the field by
        /// </summary>
        [HebrewTranslation(typeof(AggregateItem), nameof(Method))]
        Method? Method { get; set; }

        public CheckField CheckAttribute() => CheckField.Required(Attribute, Translate(nameof(Attribute)));

        public CheckField CheckAs() => CheckField.CheckString(As, Translate(nameof(As)));

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckAttribute());
            result.Fields.Add(CheckAs());

            return result.Check();
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    public interface IObjectFactory : ICipherClass
    {
        /// <summary>
        ///  List of aggregate methods defining the new object.
        ///  by default returns the grouped by fields if they 
        ///  exist. If null, returns the filtered objects
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(Aggregate))]
        List<IAggregateItem>? Aggregate { get; set; }

        /// <summary>
        /// Conditions to apply to get the desired objects. 
        /// All conditions must have the same target object.
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(Attribute))]
        IGroupedBooleanCondition Filter { get; set; }

        /// <summary>
        ///  List of object attributes to group by. 
        ///  If null, aggregates all the objects to a single one.
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(GroupBy))]
        List<string>? GroupBy { get; set; }

        /// <summary>
        /// Define order to the filtered objects
        /// </summary>
        [HebrewTranslation(typeof(ObjectFactory), nameof(OrderBy))]
        List<IOrderedItem>? OrderBy { get; set; }

        public void AddOrderBy(IOrderedItem nextOrder)
        {
            OrderBy ??= new();
            OrderBy.Add(nextOrder);
        }

        public CheckField CheckFilter()
        {
            Tuple<bool, string> result = Filter.Check();
            return new() { Succeeded = result.Item1, Message = result.Item2 };
        }

        public CheckField CheckOrderBy()
        {
            CheckField result = new();
            if (OrderBy is null) return new();
            if (OrderBy.Any()) result = CheckField.Distinct(OrderBy.Select(x => x.Attribute).ToList(), Translate(nameof(OrderBy)));
            if (result.Succeeded) result = CheckField.ListItems(OrderBy, Translate(nameof(OrderBy)));
            return result;
        }

        public CheckField CheckGroupBy()
        {
            if (GroupBy is null) return new();
            CheckField result = CheckField.Distinct(GroupBy, Translate(nameof(GroupBy)));
            if (result.Succeeded && GroupBy.Any())
            {
                foreach (string g in GroupBy)
                {
                    if (result.Succeeded) CheckField.Required(g, nameof(GroupBy));
                }
            }
            return result;
        }

        public CheckField CheckAggregate()
        {
            CheckField result = new();
            if (Aggregate is null) return new();
            if (Aggregate.Any()) result = CheckField.Distinct(Aggregate.Select(x => x.Attribute).ToList(), Translate(nameof(Aggregate)));
            if (result.Succeeded) result = CheckField.ListItems(Aggregate, Translate(nameof(Aggregate)));
            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckFilter());
            result.Fields.Add(CheckOrderBy());
            result.Fields.Add(CheckGroupBy());
            result.Fields.Add(CheckAggregate());

            return result.Check();
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
