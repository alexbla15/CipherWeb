using System.Reflection;

namespace CipherData.RandomMode
{
    public class RandomBooleanCondition: BaseBooleanCondition, IBooleanCondition
    {
        private CipherField _field = new();

        public RandomBooleanCondition() 
        {
            RandomAttribute();
            RandomAttributeRelation();
            RandomOperator();
            RandomValue();
        }

        public void RandomAttribute()
        {
            Type rootType = typeof(IEvent);

            string path1 = $"[{rootType.Name}].[{nameof(IEvent.InitialStatePackages)}].[{nameof(IPackage.Category)}].[{nameof(ICategory.Description)}]";
            string path2 = $"[{rootType.Name}].[{nameof(IEvent.InitialStatePackages)}].[{nameof(IPackage.CreatedAt)}]";
            string path3 = $"[{rootType.Name}].[{nameof(IEvent.FinalStatePackages)}].[{nameof(IPackage.BrutMass)}]";

            Attribute = RandomFuncs.RandomItem(new List<string>() { path1, path2, path3 });

            var t = CipherField.GetPathType(rootType, Attribute);

            _field = new CipherField
            {
                Path = Attribute,
                Translation = CipherField.TranslatePath(Attribute),
                FieldType = CipherField.GetPathType(rootType, Attribute)
            };
        }

        public void RandomAttributeRelation()
        {
            List<AttributeRelation> filters = CipherField.GetFilters(_field);
            AttributeRelation = RandomFuncs.RandomItem(filters);
        }

        public void RandomOperator()
        {
            Type t = typeof(Operator);
            var options = t.GetEnumValues();

            Operator = (Operator)options.GetValue(new Random().Next(options.Length));
        }

        public void RandomValue()
        {
            if (typeof(bool?).IsAssignableFrom(_field.FieldType)) Value = (new Random().Next(2) == 0).ToString();
            if (typeof(DateTime?).IsAssignableFrom(_field.FieldType)) Value = RandomFuncs.RandomDate().ToString();
            if (typeof(decimal?).IsAssignableFrom(_field.FieldType)) Value= (new Random().NextDouble()).ToString();
            if (typeof(string).IsAssignableFrom(_field.FieldType)) 
                Value = RandomFuncs.RandomItem(new List<string?>() { "AA", "BBB", "C"});
        }

        public static Type RandomRoot()
        {
            var children = CipherField.InterfaceChildren(typeof(IResource));
            return RandomFuncs.RandomItem(children);
        }

        public static CipherField RandomProperty(Type type)
        {
            List<PropertyInfo> properties = type.GetProperties().ToList();
            PropertyInfo selectedProp = RandomFuncs.RandomItem(properties);

            string path = $"[{type.Name}].[{selectedProp.Name}]";

            return new()
            {
                Path = path,
                Translation = CipherField.TranslatePath(path),
                FieldType = selectedProp.PropertyType
            };
        }
    }
}
