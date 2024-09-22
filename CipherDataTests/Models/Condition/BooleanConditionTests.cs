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
        public void EqualsTest()
        {
            // 1 - exactly the same
            BooleanCondition cond2 = cond.Copy();
            Assert.IsTrue(cond2.Equals(cond));

            // 2 - change Attribute
            cond2 = cond.Copy();
            cond2.Attribute = "b";
            Assert.IsFalse(cond2.Equals(cond));

            // 3 - change AttributeRelation
            cond2 = cond.Copy();
            cond2.AttributeRelation = AttributeRelation.Ne;
            Assert.IsFalse(cond2.Equals(cond));

            // 4 - change Operator
            cond2 = cond.Copy();
            cond2.Operator = Operator.All;
            Assert.IsFalse(cond2.Equals(cond));

            // 5 - change Value
            cond2 = cond.Copy();
            cond2.Value = "4";
            Assert.IsFalse(cond2.Equals(cond));
        }

        [TestMethod()]
        public void CheckAttributeTest()
        {
            BooleanCondition cat = new()
            {
                Attribute = "תכונה" // Good name
            };
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
            BooleanCondition c1 = cond.Copy();
            Assert.IsTrue(c1.Check().Item1);

            // 2 - bad Attribute
            c1.Attribute = "@";
            Assert.IsFalse(c1.Check().Item1);

            // 3 - bad value
            c1 = cond.Copy();
            c1.Value = "@";
            Assert.IsFalse(c1.Check().Item1);
        }
    }
}