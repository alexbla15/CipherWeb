using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class CategoryTests
    {
        [TestMethod()]
        public async Task ContainingTest()
        {
            Category cat = new();

            var result = await cat.Containing("");
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);

            result = await cat.Containing(null);
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);
        }
    }
}