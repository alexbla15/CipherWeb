using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class EventTests
    {
        [TestMethod()]
        public async Task ContainingTest()
        {
            Event ev = new();

            var result = await ev.Containing("");
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);

            result = await ev.Containing(null);
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);
        }

        [TestMethod()]
        public void StatusEventsTest()
        {
            Assert.Fail();
        }
    }
}