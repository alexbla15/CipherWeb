using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class StorageSystemTests
    {
        [TestMethod()]
        public async Task ContainingTest()
        {
            StorageSystem sys = new();

            var result = await sys.Containing("");
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);

            result = await sys.Containing(null);
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

        [TestMethod()]
        public void PackagesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void VesselsTest()
        {
            Assert.Fail();
        }
    }
}