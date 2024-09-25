using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class BooleanConditionTests
    {
        private readonly BooleanCondition cond = new()
        {
            Attribute = "A",
            AttributeRelation = AttributeRelation.Eq,
            Operator = Operator.Any,
            Value = "5"
        }; 

        [TestMethod()]
        public void CheckAttributeTest()
        {
            BooleanCondition cat = new() { Attribute = "תכונה" }; // Good name

            Assert.IsTrue(cat.CheckAttribute().Succeeded);

            cat.Attribute = "@תכונה"; // Improper chars
            Assert.IsFalse(cat.CheckAttribute().Succeeded);

            cat.Attribute = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(cat.CheckAttribute().Succeeded);

            cat.Attribute = ""; ; // Empty
            Assert.IsFalse(cat.CheckAttribute().Succeeded);

            cat.Attribute = null; ; // Null
            Assert.IsFalse(cat.CheckAttribute().Succeeded);
        }

        [TestMethod()]
        public void CheckValueTest()
        {
            BooleanCondition cat = new()
            {
                Value = "תכונה" // Good name
            };
            Assert.IsTrue(cat.CheckValue().Succeeded);

            cat.Value = "@תכונה"; // Improper chars
            Assert.IsFalse(cat.CheckValue().Succeeded);

            cat.Value = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(cat.CheckValue().Succeeded);

            cat.Value = ""; ; // Empty
            Assert.IsTrue(cat.CheckValue().Succeeded); // alowed empty

            cat.Value = null; ; // Null
            Assert.IsTrue(cat.CheckValue().Succeeded); // allowed null
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            BooleanCondition c1 = CipherClass.Copy(cond);
            Assert.IsTrue(c1.Check().Item1);

            // 2 - bad Attribute
            c1.Attribute = "@";
            Assert.IsFalse(c1.Check().Item1);

            // 3 - bad value
            c1 = CipherClass.Copy(cond);
            c1.Value = "@";
            Assert.IsFalse(c1.Check().Item1);
        }
    }
}