using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class PackageTests
    {
        [TestMethod()]
        public async Task ContainingTest()
        {
            Package pack = new();

            var result = await pack.Containing("");
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);

            result = await pack.Containing(null);
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);
        }

        [TestMethod()]
        public void EventsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ProcessesTest()
        {
            Assert.Fail();
        }
    }
}