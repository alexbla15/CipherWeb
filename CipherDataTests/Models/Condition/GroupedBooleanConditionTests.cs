using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class GroupedBooleanConditionTests
    {
        private static readonly BooleanCondition cond1 = new()
        {
            Attribute = "A",
            AttributeRelation = AttributeRelation.Eq,
            Operator = Operator.Any,
            Value = "5"
        };

        private static readonly BooleanCondition cond2 = new()
        {
            Attribute = "B",
            AttributeRelation = AttributeRelation.Ne,
            Operator = Operator.All,
            Value = "4"
        };

        private static readonly BooleanCondition cond3 = new()
        {
            Attribute = "C",
            AttributeRelation = AttributeRelation.Gt,
            Operator = Operator.Any,
            Value = "3"
        };

        private readonly GroupedBooleanCondition cond = new()
        {
            Conditions = new List<BooleanCondition> { cond1, cond2, cond3 },
            Operator = Operator.Any
        };

        [TestMethod()]
        public void GroupedBooleanConditionTest()
        {
            Assert.IsNotNull(cond);
            Assert.IsNotNull(cond.Conditions);
        }

        [TestMethod()]
        public void CheckConditionsTest()
        {
            // empty list
            GroupedBooleanCondition cond_copy = CipherClass.Copy(cond);
            cond_copy.Conditions = new List<BooleanCondition>();
            Assert.IsTrue(cond_copy.CheckConditions().Succeeded);
            cond_copy.Conditions = new List<GroupedBooleanCondition>();
            Assert.IsTrue(cond_copy.CheckConditions().Succeeded);

            // full list
            cond_copy = CipherClass.Copy(cond);
            Assert.IsTrue(cond_copy.CheckConditions().Succeeded);
            cond_copy.Conditions = new List<GroupedBooleanCondition>() { GroupedBooleanCondition.Random() };
            Assert.IsTrue(cond_copy.CheckConditions().Succeeded);

            // bad boolean condition
            BooleanCondition bool_cond = new();
            cond_copy.Conditions = new List<BooleanCondition> () { bool_cond };
            Assert.IsFalse(cond_copy.CheckConditions().Succeeded);

            // bad grouped condition
            GroupedBooleanCondition grouped_bool_cond = new()
            {
                Conditions = new List<BooleanCondition>() { bool_cond }
            };
            cond_copy.Conditions = new List<GroupedBooleanCondition>() { grouped_bool_cond };
            Assert.IsFalse(cond_copy.CheckConditions().Succeeded);
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            Assert.IsTrue(cond.Check().Item1);

            // 2 - bad conditions
            BooleanCondition bool_cond = new();
            cond.Conditions = new List<BooleanCondition>() { bool_cond };
            Assert.IsFalse(cond.Check().Item1);
        }

        [TestMethod()]
        public void RandomTest()
        {
            GroupedBooleanCondition rand_obj = GroupedBooleanCondition.Random();

            Assert.IsNotNull(rand_obj.Conditions);
        }
    }
}