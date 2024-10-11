using CipherData.Randomizer;

namespace CipherData.Models.Randomizers
{
    public class RandomCategoryProperty : ICategoryProperty
    {
        public string? Description { get; set; } = RandomFuncs.RandomItem(new List<string>() { "נבדק", "טרם נבדק", "נכשל בבדיקה" });

        public string? Name { get; set; } = RandomFuncs.RandomItem(new List<string>() { "צבע פנימי", "צבע חיצוני", "צבע כולל" });

        public PropertyType PropertyType { get; set; } = RandomFuncs.RandomItem(new List<PropertyType>() { PropertyType.Text, PropertyType.Number, PropertyType.Boolean });

        public string? DefaultValue { get; set; } = RandomFuncs.RandomItem(new List<string>() { "אדום", "5", "true" });
    }
}
