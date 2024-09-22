using CipherData.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CategoryTests
    {
        private static readonly List<string> IdMasks = new() { "1", "234", "345" };
        private static readonly List<ProcessDefinition> CreatingProcs = RandomFuncs.FillRandomObjects(3, ProcessDefinition.Random);
        private static readonly List<ProcessDefinition> ConsumingProcs = RandomFuncs.FillRandomObjects(3, ProcessDefinition.Random);

        /// <summary>
        /// minimally detailed category, without set-id
        /// </summary>
        private static readonly Category c1 = new()
        {
            Name = nameof(c1),
            Description = nameof(c1),
            IdMask = IdMasks
        };

        /// <summary>
        /// minimally detailed category, with set-id
        /// </summary>
        private static readonly Category c2 = new(nameof(c2))
        {
            Name = nameof(c2),
            Description = nameof(c2),
            IdMask = IdMasks,
            MaterialType = c1
        };

        /// <summary>
        /// fully detailed category, with set-id
        /// </summary>
        private readonly Category c3 = new(nameof(c3))
        { 
            Name = nameof(c3), 
            Description = nameof(c3),
            IdMask = IdMasks,  
            Parent = c1,
            CreatingProcesses = CreatingProcs, 
            ConsumingProcesses = ConsumingProcs,
            MaterialType = Category.Random(), 
            Children = new() { c2 },
            Properties = new() { CategoryProperty.Random()}
        };

        /// <summary>
        /// Test many issues with initiallizing category
        /// </summary>
        [TestMethod()]
        public void CategoryTest()
        {
            // try to initialize a category with and without id
            Assert.IsNotNull(c1.Id);
            Assert.IsNotNull(c2.Id);

            // Material type must be assigned by the parent, if not specified specifically
            Category c4 = new(nameof(c2))
            {
                Name = nameof(c2),
                Description = nameof(c2),
                IdMask = IdMasks,
                Parent = c2
            };
            Assert.IsTrue(c4.MaterialType == c2.MaterialType);
        }

        [TestMethod()]
        public void RequestTest()
        {
            // checking request of c3
            CategoryRequest c3_request = c3.Request();
            CategoryRequest new_request = new() { 
                Name = nameof(c3), 
                Description = nameof(c3),
                IdMask = IdMasks,
                CreatingProcesses = CreatingProcs.Select(x => x.Id).ToList(),
                ConsumingProcesses = ConsumingProcs.Select(x => x.Id).ToList(),
                ParentId = c1.Id, 
                Properties = c3.Properties
                };

            Assert.IsTrue(c3_request.Equals(new_request));
        }

        [TestMethod()]
        public void CopyTest()
        {
            Category c4 = c3.Copy();
            Assert.IsNotNull(c4);
            Assert.IsTrue(c4.Id == c3.Id);
            Assert.IsTrue(c4.Name == c3.Name);
            Assert.IsTrue(c4.Description == c3.Description);
            Assert.IsTrue(c4.Parent == c3.Parent);
            Assert.IsTrue(c4.Children?.SequenceEqual(c3.Children));
            Assert.IsTrue(c4.Properties?.SequenceEqual(c3.Properties));
            Assert.IsTrue(c4.CreatingProcesses.SequenceEqual(c3.CreatingProcesses));
            Assert.IsTrue(c4.ConsumingProcesses.SequenceEqual(c3.ConsumingProcesses));
            Assert.IsTrue(c4.MaterialType == c3.MaterialType);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            // 1 - exactly the same
            Category c4 = c3.Copy();
            Assert.IsTrue(c4.Equals(c3));

            // 2 - change name
            c4 = c3.Copy();
            c4.Name = "A";
            Assert.IsFalse(c4.Equals(c2));

            // 3 - change description
            c4 = c3.Copy();
            c4.Description = "A";
            Assert.IsFalse(c4.Equals(c2));

            // 4 - change CreatingProcesses
            c4 = c3.Copy();
            c4.CreatingProcesses = new();
            Assert.IsFalse(c4.Equals(c2));

            // 5 - change ConsumingProcesses
            c4 = c3.Copy();
            c4.ConsumingProcesses = new();
            Assert.IsFalse(c4.Equals(c2));

            // 6 - change Children
            c4 = c3.Copy();
            c4.Children = new();
            Assert.IsFalse(c4.Equals(c2));

            // 7 - change Parent
            c4 = c3.Copy();
            c4.Parent = null;
            Assert.IsFalse(c4.Equals(c2));

            // 8 - change Id
            c4 = c3.Copy();
            c4.Id = "A";
            Assert.IsFalse(c4.Equals(c2));

            // 9 - change MaterialType
            c4 = c3.Copy();
            c4.MaterialType = new();
            Assert.IsFalse(c4.Equals(c2));

            // 10 - change Properties
            c4 = c3.Copy();
            c4.Properties = new();
            Assert.IsFalse(c4.Equals(c2));
        }

        [TestMethod()]
        public void RandomTest()
        {
            // 1 - check for good instanciation of random category
            Category c4 = Category.Random();
            Assert.IsNotNull(c4.Id);
            Assert.IsNotNull(c4.Name);
            Assert.IsNotNull(c4.Description);
            Assert.IsNotNull(c4.IdMask);
            Assert.IsNotNull(c4.CreatingProcesses);
            Assert.IsNotNull(c4.ConsumingProcesses);
            Assert.IsNotNull(c4.MaterialType);
            Assert.IsNotNull(c4.Properties);

            // 2 - check for instanciation of random category with specific id
            Category c5 = Category.Random("c5");
            Assert.IsTrue(c5.Id == "c5");
        }

        [TestMethod()]
        public void RandomMaterialTypeTest()
        {
            // this function must assign an empty category, with a specific name
            // it must not have a parent / material type / creating procs / consuming procs
            Category c4 = Category.RandomMaterialType("test");

            Assert.IsTrue(c4.Name == "test");
            Assert.IsNull(c4.Parent);
            Assert.IsNull(c4.MaterialType);
            Assert.IsTrue(!c4.CreatingProcesses.Any() || c4.CreatingProcesses is null);
            Assert.IsTrue(!c4.ConsumingProcesses.Any() || c4.ConsumingProcesses is null);
        }

        [TestMethod()]
        public void GetTest()
        {
            // 1 - try to fetch object without stating id
            var result = Category.Get("");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch object with specific id
            result = Category.Get("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Item1.Equals(new("1"))); // runs through empty tests
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }

        [TestMethod()]
        public void AllTest()
        {
            var result = Category.All();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }

        [TestMethod()]
        public void ContainingTest()
        {
            // 1 - try to fetch objects without stating searched text
            var result = Category.Containing("");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch objects with specific searched text
            result = Category.Containing("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }
    }
}