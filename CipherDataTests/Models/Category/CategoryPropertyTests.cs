using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CategoryPropertyTests
    {
        [TestMethod()]
        public void CategoryPropertyTest()
        {
            CategoryProperty propWithoutValue = new(name: nameof(propWithoutValue), description: nameof(propWithoutValue));
            CategoryProperty propWithValue = new(name: nameof(propWithValue), description: nameof(propWithValue), value: "10");

            // 1 - try to initialize a category without value
            Assert.IsFalse(string.IsNullOrEmpty(propWithoutValue.Name));
            Assert.IsFalse(string.IsNullOrEmpty(propWithoutValue.Description));

            // 2 - try to initialize a category without value
            Assert.IsFalse(string.IsNullOrEmpty(propWithValue.Name));
            Assert.IsFalse(string.IsNullOrEmpty(propWithValue.Description));
            Assert.IsFalse(string.IsNullOrEmpty(propWithValue.DefaultValue));
        }

        [TestMethod()]
        public void CheckNameTest()
        {
            CategoryProperty cat = CategoryProperty.Empty();

            cat.Name = "תכונה"; // Good name
            Assert.IsTrue(cat.CheckName().Succeeded);

            cat.Name = "@תכונה"; // Improper chars
            Assert.IsFalse(cat.CheckName().Succeeded);

            cat.Name = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(cat.CheckName().Succeeded);

            cat.Name = ""; ; // Empty
            Assert.IsFalse(cat.CheckName().Succeeded);

            cat.Name = null; ; // Null
            Assert.IsFalse(cat.CheckName().Succeeded);
        }

        [TestMethod()]
        public void CheckDescriptionTest()
        {
            CategoryProperty cat = CategoryProperty.Empty();

            cat.Description = "תכונה"; // Good name
            Assert.IsTrue(cat.CheckDescription().Succeeded);

            cat.Description = "@תכונה"; // Improper chars
            Assert.IsFalse(cat.CheckDescription().Succeeded);

            cat.Description = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(cat.CheckDescription().Succeeded);

            cat.Description = ""; ; // Empty
            Assert.IsFalse(cat.CheckDescription().Succeeded);

            cat.Description = null; ; // Null
            Assert.IsFalse(cat.CheckDescription().Succeeded);
        }

        [TestMethod()]
        public void CheckDefaultValueTest()
        {
            CategoryProperty cat = CategoryProperty.Empty();

            cat.DefaultValue = "תכונה"; // Good name
            Assert.IsTrue(cat.CheckDefaultValue().Succeeded);

            cat.DefaultValue = "@תכונה"; // Improper chars
            Assert.IsFalse(cat.CheckDefaultValue().Succeeded);

            cat.DefaultValue = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(cat.CheckDefaultValue().Succeeded);

            cat.DefaultValue = ""; ; // Empty
            Assert.IsTrue(cat.CheckDefaultValue().Succeeded); // can accept empty

            cat.DefaultValue = null; ; // Null
            Assert.IsTrue(cat.CheckDefaultValue().Succeeded); // can accept nulls

            // now check for incompatible values and numeric types
            cat.PropertyType = PropertyType.Number;
            cat.DefaultValue = "5";
            Assert.IsTrue(cat.CheckDefaultValue().Succeeded);
            cat.DefaultValue = "A";
            Assert.IsFalse(cat.CheckDefaultValue().Succeeded);

            // now check for incompatible values and boolian types
            cat.PropertyType = PropertyType.Boolean;
            cat.DefaultValue = "true";
            Assert.IsTrue(cat.CheckDefaultValue().Succeeded);
            cat.DefaultValue = "A";
            Assert.IsFalse(cat.CheckDefaultValue().Succeeded);
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            CategoryProperty c1 = CategoryProperty.Random();
            Assert.IsTrue(c1.Check().Item1);

            // 2 - bad name
            c1.Name = "@";
            Assert.IsFalse(c1.Check().Item1);
            
            // 3 - bad description
            c1.Name = "a";
            c1.Description = "@";
            Assert.IsFalse(c1.Check().Item1);

            // 4 - bad default value
            c1.Description = "a";
            c1.DefaultValue = "@";
            Assert.IsFalse(c1.Check().Item1);
        }

        [TestMethod()]
        public void ToJsonTest()
        {
            // 1 - minimal data, string
            CategoryProperty cat = new(name: "A", description: "B");
            string cat_json = cat.ToJson();
            string result = "{\r\n  \"Name\": \"A\",\r\n  \"Description\": \"B\",\r\n  \"PropertyType\": 0,\r\n  \"DefaultValue\": null\r\n}";

            Assert.IsTrue(cat_json == result);

            // 2 - full data, string
            cat = new(name: "A", description: "B", propertyType: PropertyType.Text, value: "5");
            cat_json = cat.ToJson();
            result = "{\r\n  \"Name\": \"A\",\r\n  \"Description\": \"B\",\r\n  \"PropertyType\": 0,\r\n  \"DefaultValue\": \"5\"\r\n}";

            Assert.IsTrue(cat_json == result);

            // 2 - full data, numeric
            cat = new(name: "A", description: "B", propertyType: PropertyType.Number, value: "5.42");
            cat_json = cat.ToJson();
            result = "{\r\n  \"Name\": \"A\",\r\n  \"Description\": \"B\",\r\n  \"PropertyType\": 1,\r\n  \"DefaultValue\": \"5.42\"\r\n}";

            Assert.IsTrue(cat_json == result);

            // 3 - full data, boolean
            cat = new(name: "A", description: "B", propertyType: PropertyType.Boolean, value: "true");
            cat_json = cat.ToJson();
            result = "{\r\n  \"Name\": \"A\",\r\n  \"Description\": \"B\",\r\n  \"PropertyType\": 2,\r\n  \"DefaultValue\": \"true\"\r\n}";

            Assert.IsTrue(cat_json == result);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            CategoryProperty cat = new(name: "A", description: "B", propertyType: PropertyType.Number, value: "5.42");
            CategoryProperty cat2 = cat.Copy();

            Assert.IsTrue(cat.Equals(cat2));

            // 1 - different name
            cat2.Name = "B";
            Assert.IsFalse(cat.Equals(cat2));
            
            // 2 - different description
            cat2.Name = "A";
            cat2.Description = "C";
            Assert.IsFalse(cat.Equals(cat2));

            // 3 - different PropertyType
            cat2.Description = "B";
            cat2.PropertyType = PropertyType.Boolean;
            Assert.IsFalse(cat.Equals(cat2));

            // 4 - different default value
            cat2.PropertyType = PropertyType.Number;
            cat2.DefaultValue = "1";
            Assert.IsFalse(cat.Equals(cat2));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            CategoryProperty cat = new(name: "A", description: "B", propertyType: PropertyType.Number, value: "5.42");
            CategoryProperty cat2 = cat.Copy();

            Assert.IsTrue(cat.GetHashCode() == cat2.GetHashCode());

            // 1 - different name
            cat2.Name = "B";
            Assert.IsFalse(cat.GetHashCode() == cat2.GetHashCode());

            // 2 - different description
            cat2.Name = "A";
            cat2.Description = "C";
            Assert.IsFalse(cat.GetHashCode() == cat2.GetHashCode());

            // 2 - different PropertyType
            cat2.Description = "B";
            cat2.PropertyType = PropertyType.Boolean;
            Assert.IsFalse(cat.GetHashCode() == cat2.GetHashCode());

            // 2 - different default value
            cat2.PropertyType = PropertyType.Number;
            cat2.DefaultValue = "1";
            Assert.IsFalse(cat.GetHashCode() == cat2.GetHashCode());
        }

        [TestMethod()]
        public void RandomTest()
        {
            // 1 - check for good instanciation of random category-property
            CategoryProperty c = CategoryProperty.Random();
            Assert.IsNotNull(c.Check().Item1);

            // 2 - check for instanciation of random category-property with specific id
            CategoryProperty c5 = CategoryProperty.Random("c5");
            Assert.IsTrue(c5.Name == "c5");
        }

        [TestMethod()]
        public void EmptyTest()
        {
            // instanciation of an empty object scheme
            CategoryProperty EmptyCat = CategoryProperty.Empty();

            Assert.IsTrue(string.IsNullOrEmpty(EmptyCat.Name));
            Assert.IsTrue(string.IsNullOrEmpty(EmptyCat.Description));
            Assert.IsTrue(string.IsNullOrEmpty(EmptyCat.DefaultValue));
        }

        [TestMethod()]
        public void TranslateTest()
        {
            // try to translate some field of Category
            // this depends on the TranslationDictionary.json config.

            CategoryProperty EmptyCat = CategoryProperty.Empty();
            string translation = CategoryProperty.Translate(nameof(EmptyCat.DefaultValue));
            Assert.IsFalse(string.IsNullOrEmpty(translation));
            Assert.IsFalse(translation == nameof(EmptyCat.DefaultValue));
            Assert.IsTrue(translation == Translator.TranslationsDictionary[$"{nameof(CategoryProperty)}_{nameof(EmptyCat.DefaultValue)}"]);
        }
    }
}