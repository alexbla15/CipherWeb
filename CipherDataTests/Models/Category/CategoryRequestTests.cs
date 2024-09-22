using CipherData.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CategoryRequestTests
    {
        [TestMethod()]
        public void CategoryRequestTest()
        {
            // test full assignement
            CategoryRequest req = new() { Name = nameof(req), Description = nameof(req),
                IdMask = new() { "1", "22", "33" }, Properties = new(), ParentId = nameof(req)};

            Assert.IsFalse(string.IsNullOrEmpty(req.Name));
            Assert.IsFalse(string.IsNullOrEmpty(req.Description));
            Assert.IsFalse(string.IsNullOrEmpty(req.ParentId));
            Assert.IsNotNull(req.CreatingProcesses);
            Assert.IsNotNull(req.ConsumingProcesses);
            Assert.IsNotNull(req.Properties);
            Assert.IsTrue(req.IdMask.Count == 3);

            // test empty assignement
            req = new();

            Assert.IsTrue(string.IsNullOrEmpty(req.Name));
            Assert.IsTrue(string.IsNullOrEmpty(req.Description));
            Assert.IsNotNull(req.CreatingProcesses);
            Assert.IsNotNull(req.ConsumingProcesses);
            Assert.IsNull(req.Properties);
            Assert.IsNotNull(req.IdMask);
        }

        [TestMethod()]
        public void CheckNameTest()
        {
            CategoryRequest cat = new()
            {
                Name = "תכונה" // Good name
            };
            Assert.IsTrue(cat.CheckName().Succeeded);

            cat.Name = "@תכונה"; // Improper chars
            Assert.IsFalse(cat.CheckName().Succeeded);

            cat.Name = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(cat.CheckName().Succeeded);

            cat.Name = ""; ; // Empty
            Assert.IsFalse(cat.CheckName().Succeeded);

            cat.Name = null; ; // Null
            Assert.IsFalse(cat.CheckName().Succeeded);
        }

        [TestMethod()]
        public void CheckDescriptionTest()
        {
            CategoryRequest cat = new()
            {
                Description = "תכונה" // Good name
            };
            Assert.IsTrue(cat.CheckDescription().Succeeded);

            cat.Description = "@תכונה"; // Improper chars
            Assert.IsFalse(cat.CheckDescription().Succeeded);

            cat.Description = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(cat.CheckDescription().Succeeded);

            cat.Description = ""; ; // Empty
            Assert.IsFalse(cat.CheckDescription().Succeeded);

            cat.Description = null; ; // Null
            Assert.IsFalse(cat.CheckDescription().Succeeded);
        }

        [TestMethod()]
        public void CheckIdMaskTest()
        {
            CategoryRequest cat = new();
            
            // 1 - empty list
            cat.IdMask = new();
            Assert.IsFalse(cat.CheckIdMask().Succeeded);

            // 2 - full list
            cat.IdMask = new() { "1", "2"};
            Assert.IsTrue(cat.CheckIdMask().Succeeded);
        }

        [TestMethod()]
        public void CheckCreatingProcessesTest()
        {
            CategoryRequest cat = new();

            // 1 - empty list
            cat.CreatingProcesses = new();
            Assert.IsFalse(cat.CheckCreatingProcesses().Succeeded);

            // 2 - full list
            cat.CreatingProcesses = new() { "1", "2" };
            Assert.IsTrue(cat.CheckCreatingProcesses().Succeeded);
        }

        [TestMethod()]
        public void CheckConsumingProcessesTest()
        {
            CategoryRequest cat = new();

            // 1 - empty list
            cat.ConsumingProcesses = new();
            Assert.IsFalse(cat.CheckConsumingProcesses().Succeeded);

            // 2 - full list
            cat.ConsumingProcesses = new() { "1", "2" };
            Assert.IsTrue(cat.CheckConsumingProcesses().Succeeded);
        }

        [TestMethod()]
        public void CheckPropertiesTest()
        {
            CategoryRequest cat = new();

            // 1 - null
            cat.Properties = null;
            Assert.IsTrue(cat.CheckProperties().Succeeded);

            // 2 - empty
            cat.Properties = new();
            Assert.IsTrue(cat.CheckProperties().Succeeded);

            // 3 - duplicity
            CategoryProperty c = CategoryProperty.Random();
            cat.Properties = new() { c, c };
            Assert.IsFalse(cat.CheckProperties().Succeeded);

            // 4 - Bad property
            c.Name = "@";
            cat.Properties = new() { c };
            Assert.IsFalse(cat.CheckProperties().Succeeded);

            // 4 - All Good
            c.Name = "A";
            cat.Properties = new() { c, CategoryProperty.Random() };
            Assert.IsTrue(cat.CheckProperties().Succeeded);
        }

        [TestMethod()]
        public void CheckParentIdTest()
        {
            CategoryRequest cat = new();

            cat.ParentId = "1"; // Good id
            Assert.IsTrue(cat.CheckParentId().Succeeded);

            cat.ParentId = "@1"; // Improper chars
            Assert.IsFalse(cat.CheckParentId().Succeeded);

            cat.ParentId = "SELect1"; ; // Improper words
            Assert.IsFalse(cat.CheckParentId().Succeeded);

            cat.ParentId = ""; ; // Empty
            Assert.IsTrue(cat.CheckParentId().Succeeded); // empty is allowed

            cat.ParentId = null; ; // Null
            Assert.IsTrue(cat.CheckParentId().Succeeded); // null is allowed
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            CategoryRequest req = new()
            {
                Name = nameof(req),
                Description = nameof(req),
                IdMask = new() { "1", "22", "33" },
                CreatingProcesses = new() { "1", "2" },
                ConsumingProcesses = new() { "1", "2" },
                Properties = new(),
                ParentId = nameof(req)
            };
            Assert.IsTrue(req.Check().Item1);

            // 2 - bad name
            req.Name = "@";
            Assert.IsFalse(req.Check().Item1);

            // 3 - bad description
            req.Name = "a";
            req.Description = "@";
            Assert.IsFalse(req.Check().Item1);

            // 4 - bad Description
            req.Description = "a";
            req.IdMask = new();
            Assert.IsFalse(req.Check().Item1);

            // 5 - bad creatingProcesses
            req.IdMask = new() { "1", "22", "33" };
            req.CreatingProcesses = new();
            Assert.IsFalse(req.Check().Item1);

            // 6 - bad consumingProcesses
            req.CreatingProcesses = new() { "1", "22", "33" };
            req.ConsumingProcesses = new();
            Assert.IsFalse(req.Check().Item1);

            // 7 - bad properties
            req.ConsumingProcesses = new() { "1", "22", "33" };
            CategoryProperty c = CategoryProperty.Random();
            req.Properties = new() { c, c };
            Assert.IsFalse(req.Check().Item1);

            // 8 - bad parent
            req.Properties = new();
            req.ParentId = "@";
            Assert.IsFalse(req.Check().Item1);
        }

        [TestMethod()]
        public void CopyTest()
        {
            CategoryRequest req = new()
            {
                Name = nameof(req),
                Description = nameof(req),
                IdMask = new() { "1", "22", "33" },
                Properties = new(),
                ParentId = nameof(req)
            };

            CategoryRequest req2 = req.Copy();

            Assert.IsNotNull(req2);
            Assert.IsTrue(req2.Name == req.Name);
            Assert.IsTrue(req2.Description == req.Description);
            Assert.IsTrue(req2.ParentId == req.ParentId);
            Assert.IsTrue(req2.Properties?.SequenceEqual(req.Properties));
            Assert.IsTrue(req2.CreatingProcesses.SequenceEqual(req.CreatingProcesses));
            Assert.IsTrue(req2.ConsumingProcesses.SequenceEqual(req.ConsumingProcesses));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            CategoryRequest req = new()
            {
                Name = nameof(req),
                Description = nameof(req),
                IdMask = new() { "1", "22", "33" },
                CreatingProcesses = new() { "1", "2" },
                ConsumingProcesses = new() { "1", "2" },
                Properties = new(),
                ParentId = nameof(req)
            };

            CategoryRequest req2 = req.Copy();

            Assert.IsTrue(req.Equals(req2));

            // 1 - different name
            req2.Name = "B";
            Assert.IsFalse(req.Equals(req2));

            // 2 - different description
            req2 = req.Copy();
            req2.Description = "C";
            Assert.IsFalse(req.Equals(req2));

            // 3 - different IdMask
            req2 = req.Copy();
            req2.IdMask = new() { "1", "2", "33" };
            Assert.IsFalse(req.Equals(req2));

            // 4 - different CreatingProcesses
            req2 = req.Copy(); 
            req2.CreatingProcesses = new() { "1", "3" };
            Assert.IsFalse(req.Equals(req2));

            // 5 - different ConsumingProcesses
            req2 = req.Copy();
            req2.ConsumingProcesses = new() { "1", "3" };
            Assert.IsFalse(req.Equals(req2));

            // 6 - different Properties
            req2 = req.Copy();
            req2.Properties = new() { CategoryProperty.Random() };
            Assert.IsFalse(req.Equals(req2));

            // 7 - different Parent
            req2 = req.Copy();
            req2.ParentId = "1";
            Assert.IsFalse(req.Equals(req2));

            // 8 - null
            Assert.IsFalse(req.Equals(null));
        }

        [TestMethod()]
        public void DifferentTest()
        {
            List<string> IdMasks = new() { "1", "234", "345" };
            List<ProcessDefinition> CreatingProcs = RandomFuncs.FillRandomObjects(3, ProcessDefinition.Random);
            List<ProcessDefinition> ConsumingProcs = RandomFuncs.FillRandomObjects(3, ProcessDefinition.Random);

            Category cat = new(nameof(cat))
            {
                Name = nameof(cat),
                Description = nameof(cat),
                IdMask = IdMasks,
                Parent = Category.Random()
            };

            CategoryRequest req = cat.Request();

            Assert.IsFalse(req.Different(cat));

            // 1 - different name
            req.Name = "B";
            Assert.IsTrue(req.Different(cat));

            // 2 - different description
            req.Name = nameof(cat);
            req.Description = "C";
            Assert.IsTrue(req.Different(cat));

            // 3 - different IdMask
            req.Description = nameof(req);
            req.IdMask = new() { "1", "2", "33" };
            Assert.IsTrue(req.Different(cat));

            // 4 - different CreatingProcesses
            req.IdMask = IdMasks;
            req.CreatingProcesses = new() { "1", "3" };
            Assert.IsTrue(req.Different(cat));

            // 5 - different ConsumingProcesses
            req.CreatingProcesses = CreatingProcs.Select(x=>x.Id).ToList();
            req.ConsumingProcesses = new() { "1", "3" };
            Assert.IsTrue(req.Different(cat));

            // 6 - different Properties
            req.ConsumingProcesses = ConsumingProcs.Select(x => x.Id).ToList();
            req.Properties = new() { CategoryProperty.Random() };
            Assert.IsTrue(req.Different(cat));

            // 7 - different Parent
            req.Properties = new();
            req.ParentId = "001";
            Assert.IsTrue(req.Different(cat));

            // 8 - null
            Assert.IsTrue(req.Different(null));
        }

        [TestMethod()]
        public void CreateTest()
        {

            Category cat = new("2")
            {
                Name = nameof(cat),
                Description = nameof(cat),
                IdMask = new() { "1", "2" },
                Parent = Category.Random("C001")
            };

            CategoryRequest req = cat.Request();

            Category cat2 = req.Create("2");

            Assert.IsTrue(cat2.Id == "2");
            Assert.IsTrue(req.Name == cat2.Name);
            Assert.IsTrue(req.Description == cat2.Description);
            Assert.IsTrue(req.IdMask == cat2.IdMask);
            Assert.IsTrue(req.CreatingProcesses.SequenceEqual(cat2.CreatingProcesses.Select(x=>x.Id).ToList()));
            Assert.IsTrue(req.ConsumingProcesses.SequenceEqual(cat2.ConsumingProcesses.Select(x=>x.Id).ToList()));
            Assert.IsTrue(req.ParentId == cat2.Parent?.Id);
        }
    }
}