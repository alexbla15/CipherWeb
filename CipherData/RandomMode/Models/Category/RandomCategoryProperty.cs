namespace CipherData.RandomMode
{
    public class RandomCategoryProperty : BaseCategoryProperty, ICategoryProperty
    {
        public RandomCategoryProperty(string? name, string? description, PropertyType propType, string? value)
        {
            Name = name;
            Description = description;
            PropertyType = propType;
            DefaultValue = value;
        }

        public RandomCategoryProperty()
        {
            RandomCategoryProperty chosenObject = RandomFuncs.RandomItem(new List<RandomCategoryProperty> { r1, r2, r3 });
            Name = chosenObject.Name;
            Description = chosenObject.Description;
            PropertyType = chosenObject.PropertyType;
            DefaultValue = chosenObject.DefaultValue;
        }

        public static readonly RandomCategoryProperty r1 = 
            new("צבע חיצוני", "נבדק", PropertyType.Text, "אדום");

        public static readonly RandomCategoryProperty r2 =
            new("צבע פנימי", "משך בדיקה", PropertyType.Number, "5" );

        public static readonly RandomCategoryProperty r3 =
            new("צבע כולל", "נכשל בבדיקה", PropertyType.Boolean, "true");
    }
}
