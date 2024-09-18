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
        private static readonly Category c1 = new(name: nameof(c1), description: nameof(c1),
                idMask: IdMasks, creatingProcesses: new(), consumingProcesses: new());

        /// <summary>
        /// minimally detailed category, with set-id
        /// </summary>
        private static readonly Category c2 = new(id: nameof(c2), name: nameof(c2), description: nameof(c2),
                idMask: IdMasks, creatingProcesses: new(), consumingProcesses: new(),
                materialType: c1);

        /// <summary>
        /// fully detailed category, with set-id
        /// </summary>
        private readonly Category c3 = new(id: nameof(c3), name: nameof(c3), description: nameof(c3),
                idMask: IdMasks,  parent: c1,
                creatingProcesses: CreatingProcs, consumingProcesses: ConsumingProcs,
                materialType: Category.Random(), children: new () { c2},
                properties: new() { CategoryProperty.Random()});

        /// <summary>
        /// Test many issues with initiallizing category
        /// </summary>
        [TestMethod()]
        public void CategoryTest()
        {
            // try to initialize a category with and without id
            Assert.IsTrue(c1.Id != null);
            Assert.IsTrue(c2.Id != null);

            // Material type must be assigned by the parent, if not specified specifically
            Category c4  = new(id: nameof(c2), name: nameof(c2), description: nameof(c2),
                idMask: IdMasks, creatingProcesses: new(), consumingProcesses: new(),
                parent: c2);
            Assert.IsTrue(c4.MaterialType == c2.MaterialType);
        }

        [TestMethod()]
        public void RequestTest()
        {
            // checking request of c3
            CategoryRequest c3_request = c3.Request();
            CategoryRequest new_request = new(name: nameof(c3), description: nameof(c3),
                idMask: IdMasks,
                creatingProcesses: CreatingProcs.Select(x => x.Id).ToList(),
                consumingProcesses: ConsumingProcs.Select(x => x.Id).ToList(),
                parent: c1.Id, properties: c3.Properties
                );

            Assert.IsTrue(c3_request.Name == new_request.Name);
            Assert.IsTrue(c3_request.Description == new_request.Description);
            Assert.IsTrue(c3_request.IdMask == new_request.IdMask);
            Assert.IsTrue(c3_request.CreatingProcesses.SequenceEqual(new_request.CreatingProcesses));
            Assert.IsTrue(c3_request.ConsumingProcesses.SequenceEqual(new_request.ConsumingProcesses));
            Assert.IsTrue(c3_request.ParentId == new_request.ParentId);
            Assert.IsTrue(c3_request.Properties == new_request.Properties);
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
            Assert.IsTrue(c4.Children.SequenceEqual(c3.Children));
            Assert.IsTrue(c4.Properties.SequenceEqual(c4.Properties));
            Assert.IsTrue(c4.CreatingProcesses.SequenceEqual(c4.CreatingProcesses));
            Assert.IsTrue(c4.ConsumingProcesses.SequenceEqual(c4.ConsumingProcesses));
            Assert.IsTrue(c4.MaterialType == c3.MaterialType);
        }

        [TestMethod()]
        public void HeadersTest()
        {
            int resource_headers = Resource.Headers().Count;
            List<string> category_headers = new() { nameof(c3.Name), nameof(c3.Description),
            nameof(c3.Properties), nameof(c3.Children), nameof(c3.Parent), nameof(c3.ConsumingProcesses),
            nameof(c3.CreatingProcesses), nameof(c3.IdMask), nameof(c3.MaterialType)};
            Assert.IsTrue(Category.Headers().Count == (resource_headers + category_headers.Count));
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
        public void EmptyTest()
        {
            // instanciation of an empty Category scheme
            Category EmptyCat = Category.Empty();

            Assert.IsTrue(string.IsNullOrEmpty(EmptyCat.Id));
            Assert.IsTrue(string.IsNullOrEmpty(EmptyCat.Name));
            Assert.IsTrue(string.IsNullOrEmpty(EmptyCat.Description));
            Assert.IsTrue(!EmptyCat.IdMask.Any() || EmptyCat.IdMask is null);
            Assert.IsTrue(!EmptyCat.CreatingProcesses.Any() || EmptyCat.CreatingProcesses is null);
            Assert.IsTrue(!EmptyCat.ConsumingProcesses.Any() || EmptyCat.ConsumingProcesses is null);
            Assert.IsNull(EmptyCat.Parent);
            Assert.IsNull(EmptyCat.Children);
            Assert.IsNull(EmptyCat.MaterialType);
            Assert.IsNull(EmptyCat.Properties);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Category c4 = c3.Copy();
            Assert.IsTrue(c4.Equals(c3));
            Assert.IsFalse(c4.Equals(c2));
        }

        [TestMethod()]
        public void TranslateTest()
        {
            // try to translate some field of Category
            // this depends on the TranslationDictionary.json config.

            string translation = Category.Translate(nameof(c3.Parent));
            Assert.IsFalse(string.IsNullOrEmpty(translation));
            Assert.IsFalse(translation == nameof(c3.Parent));
            Assert.IsTrue(translation == Translator.TranslationsDictionary[$"{nameof(Category)}_{nameof(c3.Parent)}"]);
        }

        [TestMethod()]
        public void GetTest()
        {
            // 1 - try to fetch category without stating id
            var result = Category.Get("");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Equals(Category.Empty())); // runs through empty tests
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch category with specific id
            result = Category.Get("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Item1.Equals(Category.Empty())); // runs through empty tests
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
            // 1 - try to fetch categories without stating searched text
            var result = Category.Containing("");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch categories with specific searched text
            result = Category.Containing("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }
    }
}