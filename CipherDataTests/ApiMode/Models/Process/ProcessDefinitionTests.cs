using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class ProcessDefinitionTests
    {
        [TestMethod()]
        public async Task ContainingTest()
        {
            ProcessDefinition proc = new();

            var result = await proc.Containing("");
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);

            result = await proc.Containing(null);
            Assert.IsTrue(result.Item2 == General.ErrorResponse.BadRequest);
        }
    }
}