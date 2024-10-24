using CipherData.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class CategoryPropertyTests
    {
        [TestMethod()]
        public void GetHashCodeTest()
        {
            CategoryProperty c1 = new() { 
                Name = "A",
                Description = "B", 
                PropertyType = 
                PropertyType.Text, 
                DefaultValue = "5" };

            CategoryProperty c2 = new()
            {
                Name = "B",
                Description = "B",
                PropertyType =
                PropertyType.Text,
                DefaultValue = "5"
            };

            CategoryProperty c3 = new()
            {
                Name = "A",
                Description = "C",
                PropertyType =
                PropertyType.Text,
                DefaultValue = "5"
            };

            CategoryProperty c4 = new()
            {
                Name = "A",
                Description = "B",
                PropertyType =
                PropertyType.Number,
                DefaultValue = "5"
            };

            CategoryProperty c5 = new()
            {
                Name = "A",
                Description = "B",
                PropertyType =
                PropertyType.Text,
                DefaultValue = "6"
            };

            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode());
            Assert.AreNotEqual(c1.GetHashCode(), c3.GetHashCode());
            Assert.AreNotEqual(c1.GetHashCode(), c4.GetHashCode());
            Assert.AreNotEqual(c1.GetHashCode(), c5.GetHashCode());
        }
    }
}