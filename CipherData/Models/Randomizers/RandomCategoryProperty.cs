namespace CipherData.Models.Randomizers
{
    public class RandomCategoryProperty : ICategoryProperty
    {
        private static readonly int RandId = new Random().Next(3);

        public string? Description { get; set; } = new List<string>() { "צבע נראה לעין", "כמות יחידות", "עבר בדיקה, כעת מוכן לאיחסון" }[RandId];

        public string? Name { get; set; } = new List<string>() { "צבע", "כמות", "מיועד לאיחסון" }[RandId];

        public PropertyType PropertyType { get; set; } = new List<PropertyType>() { PropertyType.Text, PropertyType.Number, PropertyType.Boolean }[RandId];

        public string? DefaultValue { get; set; } = new List<string>() { "אדום", "5", "True" }[RandId];
    }
}
