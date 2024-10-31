using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class ProcessTests
    {
        [TestMethod()]
        public async Task ContainingTest()
        {
            Process proc = new();

            var result = await proc.Containing("");
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);

            result = await proc.Containing(null);
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);
        }
    }
}