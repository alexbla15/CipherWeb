using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class PackageTests
    {
        private static readonly StorageSystem s = StorageSystem.Random();
        private static readonly Package parent = Package.Random();
        private static readonly Category cat = Category.Random();
        private static readonly Vessel ves = Vessel.Random();

        private static readonly Package example = new()
        {
            System = s,
            BrutMass = 10,
            NetMass = 5,
            CreatedAt = DateTime.Now,
            Vessel = ves,
            Parent = parent,
            Children = new(),
            Description = "A",
            Properties = new(),
            Category = cat
        };

        [TestMethod()]
        public void PackageTest()
        {
            // initialize a object without id
            Assert.IsNotNull(example.Id);
            Assert.IsNotNull(example.Parent);
            Assert.IsNotNull(example.Vessel);
            Assert.IsNotNull(example.Children);
            Assert.IsNotNull(example.DestinationProcesses);
            Assert.IsNotNull(example.Description);
            Assert.IsNotNull(example.Properties);
            Assert.IsNotNull(example.Category);

            // initialize with id
            Package p2 = new(id:"1")
            {
                System = s,
                BrutMass = 10,
                NetMass = 5,
                CreatedAt = DateTime.Now,
                Vessel = ves,
                Parent = parent,
                Children = new(),
                Description = "A",
                Properties = new(),
                Category = cat,
                DestinationProcesses = new() { ProcessDefinition.Random()}
            };

            Assert.IsNotNull(p2.Id);
            Assert.AreEqual(p2.Id, "1");
            Assert.IsNotNull(p2.DestinationProcesses);
            Assert.IsFalse(p2.DestinationProcesses.SequenceEqual(p2.Category.ConsumingProcesses));

            // initialize without destination processes
            Package p3 = new(id: "1")
            {
                System = s,
                BrutMass = 10,
                NetMass = 5,
                CreatedAt = DateTime.Now,
                Vessel = ves,
                Parent = parent,
                Children = new(),
                Description = "A",
                Properties = new(),
                Category = cat
            };
            Assert.IsNotNull(p3.DestinationProcesses);
            Assert.IsTrue(p3.DestinationProcesses.SequenceEqual(p3.Category.ConsumingProcesses));
        }

        [TestMethod()]
        public void GetNextIdTest()
        {
            Assert.IsFalse(string.IsNullOrEmpty(Package.GetNextId()));
        }

        [TestMethod()]
        public void RandomTest()
        {
            // 1 - check for good instanciation of random category
            Package p = Package.Random();
            Assert.IsNotNull(p.Id);
            Assert.IsNotNull(p.System);
            Assert.IsNotNull(p.Description);
            Assert.IsNotNull(p.Category);

            // 2 - check for instanciation of random category with specific id
            Package p2 = Package.Random("1");
            Assert.IsNotNull(p2.Id);
            Assert.AreEqual(p2.Id, "1");
        }

        [TestMethod()]
        public void RequestTest()
        {
            // checking request of c3
            PackageRequest req = example.Request();
            PackageRequest new_request = new()
            {
                Id = example.Id,
                SystemId = s.Id,
                BrutMass = example.BrutMass,
                NetMass = example.NetMass,
                CategoryId = cat.Id,
                VesselId = ves.Id,
                ChildrenIds = example.Children?.Select(x => x.Id).ToList(),
                ParentId = parent.Id,
                Properties = example.Properties
            };

            Assert.IsTrue(req.Equals(new_request));
        }

        [TestMethod()]
        public void EventsTest()
        {
            var result = example.Events();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }

        [TestMethod()]
        public void ProcessesTest()
        {
            var result = example.Processes();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }

        [TestMethod()]
        public void GetTest()
        {
            // 1 - try to fetch object without stating id
            var result = Package.Get(string.Empty);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch object with specific id
            result = Package.Get("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Item1.Equals(new())); // runs through empty tests
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }

        [TestMethod()]
        public void AllTest()
        {
            var result = Package.All();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }

        [TestMethod()]
        public void ContainingTest()
        {
            // 1 - try to fetch objects without stating searched text
            var result = Package.Containing("");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch objects with specific searched text
            result = Package.Containing("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }
    }
}