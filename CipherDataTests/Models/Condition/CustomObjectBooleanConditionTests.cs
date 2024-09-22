using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CustomObjectBooleanConditionTests
    {
        private readonly CustomObjectBooleanCondition cond = new();


        [TestMethod()]
        public void CustomObjectBooleanConditionTest()
        {
            Assert.IsNotNull(cond);
            Assert.IsNotNull(cond.Conditions);
        }

        [TestMethod()]
        public void RandomTest()
        {
            CustomObjectBooleanCondition rand_obj = CustomObjectBooleanCondition.Random();

            Assert.IsNotNull(rand_obj.Conditions);
        }
    }
}