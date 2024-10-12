using CipherData.ApiMode.Models.Category;
using CipherData.Interfaces.Models.Category;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CategoryPropertyTests
    {
        private readonly CategoryProperty propWithoutValue = new() { Name = nameof(propWithoutValue), Description = nameof(propWithoutValue) };
        private readonly CategoryProperty propWithValue = new() { Name = nameof(propWithValue), Description = nameof(propWithValue), DefaultValue = "10" };

        [TestMethod()]
        public void CategoryPropertyTest()
        {
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
            ICategoryProperty cat = new CategoryProperty() { Name = "תכונה" }; // Good name
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
            CategoryProperty cat = new() { Description = "תכונה" }; 
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
            CategoryProperty cat = new() { DefaultValue = "תכונה" };
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
        public void GetHashCodeTest()
        {
            CategoryProperty cat = new() { Name = "A", Description = "B", PropertyType = PropertyType.Number, DefaultValue = "5.42"};
            CategoryProperty cat2 = CipherClass.Copy(cat);

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
    }
}