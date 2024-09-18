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

        private static readonly Package example = new(system: s, brutMass: 10, netMass: 5, createdAt: DateTime.Now,
                vessel: ves, parent: parent, children: new(), destinationProcesses: new(),
                description: "A", properties: new(), category: cat);

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
            Package p2 = new(system: s, brutMass: 10, netMass: 5, createdAt: DateTime.Now,
                vessel: ves, parent: parent, children: new(), destinationProcesses: new(),
                description: "A", properties: new(), category: cat, id : "1");

            Assert.IsNotNull(p2.Id);
            Assert.AreEqual(p2.Id, "1");

            // initialize without destination processes
            Package p3 = new(system: s, brutMass: 10, netMass: 5, createdAt: DateTime.Now,
                vessel: ves, parent: parent, children: new(), description: "A", properties: new(), category: cat, id: "1");

            Assert.IsNotNull(p3.DestinationProcesses);
            Assert.IsTrue(p3.DestinationProcesses.SequenceEqual(p3.Category.ConsumingProcesses));
        }

        [TestMethod()]
        public void GetNextIdTest()
        {
            Assert.IsFalse(string.IsNullOrEmpty(Package.GetNextId()));
        }

        [TestMethod()]
        public void HeadersTest()
        {
            int resource_headers = Resource.Headers().Count;
            List<string> pack_headers = new() { nameof(example.CreatedAt), nameof(example.System),
            nameof(example.Properties), nameof(example.Children), nameof(example.Parent), nameof(example.DestinationProcesses),
            nameof(example.BrutMass), nameof(example.NetMass), nameof(example.Vessel), nameof(example.Description),
                nameof(example.Category),};
            Assert.IsTrue(Package.Headers().Count == (resource_headers + pack_headers.Count));
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
            PackageRequest new_request = new(id: example.Id, system: s.Id,
                brutMass: example.BrutMass, netMass: example.NetMass, category: cat.Id,
                vessel: ves.Id, children: example.Children?.Select(x => x.Id).ToList(),
                parent: parent.Id, properties: example.Properties);

            Assert.IsTrue(req.Equals(new_request));
        }

        [TestMethod()]
        public void EmptyTest()
        {
            // instanciation of an empty Category scheme
            Package p = Package.Empty();

            Assert.IsTrue(string.IsNullOrEmpty(p.Id));
            Assert.AreEqual(p.BrutMass, 0);
            Assert.AreEqual(p.NetMass, 0);
            Assert.IsTrue(p.Category.Equals(Category.Empty()));
            Assert.IsTrue(p.System.Equals(StorageSystem.Empty()));
            Assert.IsNull(p.Parent);
            Assert.IsNull(p.Children);
            Assert.IsNull(p.Vessel);
            Assert.IsFalse(p.DestinationProcesses.Any());
            Assert.IsNull(p.Properties);
        }

        [TestMethod()]
        public void TranslateTest()
        {
            // try to translate some field of Category
            // this depends on the TranslationDictionary.json config.

            string translation = Package.Translate(nameof(example.Parent));
            Assert.IsFalse(string.IsNullOrEmpty(translation));
            Assert.IsFalse(translation == nameof(example.Parent));
            Assert.IsTrue(translation == Translator.TranslationsDictionary[$"{nameof(Package)}_{nameof(example.Parent)}"]);
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
            var result = Package.Get("");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Equals(Package.Empty())); // runs through empty tests
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch object with specific id
            result = Package.Get("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Item1.Equals(Package.Empty())); // runs through empty tests
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