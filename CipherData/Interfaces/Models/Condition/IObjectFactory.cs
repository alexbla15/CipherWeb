using System.Reflection;
using System.Text.Json;

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

    [HebrewTranslation(nameof(IOrderedItem))]
    public interface IOrderedItem : ICipherClass
    {
        /// <summary>
        /// Attribute to order by
        /// </summary>
        [HebrewTranslation(typeof(IOrderedItem), nameof(Attribute))]
        [Check(CheckRequirement.Required)]
        string Attribute { get; set; }

        /// <summary>
        /// Desired order on the attribute
        /// </summary>
        [HebrewTranslation(typeof(IOrderedItem), nameof(Order))]
        Order Order { get; set; }

        public CheckField CheckAttribute() => CheckProperty(this, nameof(Attribute));

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new()
            {
                Fields = new() { CheckAttribute() }
            };

            return result.Check();
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    [HebrewTranslation(nameof(IAggregateItem))]
    public interface IAggregateItem : ICipherClass
    {
        /// <summary>
        /// New name to give to the aggregated field. 
        /// if null, name is auto generated
        /// </summary>
        [HebrewTranslation(typeof(IAggregateItem), nameof(As))]
        [Check(CheckRequirement.Text)]
        string? As { get; set; }

        /// <summary>
        /// Attribute path to aggregate on
        /// </summary>
        [HebrewTranslation(typeof(IAggregateItem), nameof(Attribute))]
        [Check(CheckRequirement.Required)]
        string? Attribute { get; set; }

        /// <summary>
        /// Method to aggregate the field by
        /// </summary>
        [HebrewTranslation(typeof(IAggregateItem), nameof(Method))]
        Method? Method { get; set; }

        public CheckField CheckAttribute() => CheckProperty(this, nameof(Attribute));

        public CheckField CheckAs() => CheckProperty(this, nameof(As));

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

    /// <summary>
    /// Method to get desired objects by filtering and aggregating the database
    /// </summary>
    [HebrewTranslation(nameof(IObjectFactory))]
    public interface IObjectFactory : ICipherClass
    {
        /// <summary>
        ///  List of aggregate methods defining the new object.
        ///  by default returns the grouped by fields if they 
        ///  exist. If null, returns the filtered objects
        /// </summary>
        [HebrewTranslation(typeof(IObjectFactory), nameof(Aggregate))]
        [Check(CheckRequirement.List, distinct:true, checkItems:true)]
        List<IAggregateItem>? Aggregate { get; set; }

        /// <summary>
        /// Conditions to apply to get the desired objects. 
        /// All conditions must have the same target object.
        /// </summary>
        [HebrewTranslation(typeof(IObjectFactory), nameof(Attribute))]
        IGroupedBooleanCondition Filter { get; set; }

        /// <summary>
        ///  List of object attributes to group by. 
        ///  If null, aggregates all the objects to a single one.
        /// </summary>
        [HebrewTranslation(typeof(IObjectFactory), nameof(GroupBy))]
        List<string>? GroupBy { get; set; }

        /// <summary>
        /// Define order to the filtered objects
        /// </summary>
        [HebrewTranslation(typeof(IObjectFactory), nameof(OrderBy))]
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

        public CheckField CheckAggregate() => CheckProperty(this, nameof(Aggregate));

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
        
        public IObjectFactory Export()
        {
            IObjectFactory? newItem = Config.ObjectFactory();

            newItem.Aggregate = Aggregate;
            newItem.GroupBy = GroupBy;
            newItem.OrderBy = OrderBy;
            newItem.Filter = Filter.Export();

            return newItem;
        }

        public new string ToJson()
            => JsonSerializer.Serialize(Export(), typeof(IObjectFactory), JsonOptions);

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
