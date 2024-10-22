namespace CipherData.RandomMode
{
    public class RandomCategoryProperty : BaseCategoryProperty, ICategoryProperty
    {
        public RandomCategoryProperty()
        {
            Description = RandomFuncs.RandomItem(new List<string>() { "נבדק", "טרם נבדק", "נכשל בבדיקה" });
            Name = RandomFuncs.RandomItem(new List<string>() { "צבע פנימי", "צבע חיצוני", "צבע כולל" });
            PropertyType = RandomFuncs.RandomItem(new List<PropertyType>() { PropertyType.Text, PropertyType.Number, PropertyType.Boolean });
            DefaultValue = RandomFuncs.RandomItem(new List<string>() { "אדום", "5", "true" });
        }
    }
}
