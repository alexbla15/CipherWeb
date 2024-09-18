using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class EventTests
    {
        private static readonly Event example = new(worker: "א", timestamp: DateTime.Now, packages: new() { Package.Random() },
                processId: "b", comments: "c", eventType: 1, status: 0);

        [TestMethod()]
        public void EventTest()
        {
            // 1 - without stating id
            Assert.IsNotNull(example.Id);
            Assert.IsNotNull(example.Worker);
            Assert.IsNotNull(example.ProcessId);
            Assert.IsNotNull(example.Comments);

            // 2 - with stating id
            Event ev = new(worker: "א", timestamp: DateTime.Now, packages: new() { Package.Random() },
                processId: "b", comments: "c", eventType: 1, status: 0, id: "1");

            Assert.IsNotNull(ev.Id);
            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.ProcessId);
            Assert.IsNotNull(ev.Comments);
        }

        [TestMethod()]
        public void EmptyTest()
        {
            // instanciation of an empty object scheme
            Event ev = Event.Empty();

            Assert.IsTrue(string.IsNullOrEmpty(ev.Comments));
            Assert.IsTrue(string.IsNullOrEmpty(ev.ProcessId));
            Assert.IsTrue(string.IsNullOrEmpty(ev.Worker));
            Assert.IsTrue(ev.EventType == 0);
            Assert.IsTrue(ev.Status == 0);
            Assert.IsFalse(ev.Packages.Any());
        }

        [TestMethod()]
        public void GetNextIdTest()
        {
            Assert.IsFalse(string.IsNullOrEmpty(Event.GetNextId()));
        }

        [TestMethod()]
        public void HeadersTest()
        {
            int resource_headers = Resource.Headers().Count;
            List<string> event_headers = new() { nameof(example.Worker), nameof(example.Timestamp),
            nameof(example.Packages), nameof(example.ProcessId), nameof(example.Comments), nameof(example.EventType),
            nameof(example.Status)};
            Assert.IsTrue(Event.Headers().Count == (resource_headers + event_headers.Count));
        }

        [TestMethod()]
        public void RandomTest()
        {
            // 1 - check for good instanciation of random object
            Event ev = Event.Random();
            Assert.IsNotNull(ev.Id);
            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.ProcessId);
            Assert.IsNotNull(ev.Comments);
            Assert.IsFalse(ev.EventType == 0);

            // 2 - check for instanciation of random object with specific id
            ev = Event.Random("1");
            Assert.IsNotNull(ev.Id);
            Assert.IsTrue(ev.Id == "1");
            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.ProcessId);
            Assert.IsNotNull(ev.Comments);
            Assert.IsFalse(ev.EventType == 0);
        }

        [TestMethod()]
        public void TranslateTest()
        {
            // try to translate some field
            // this depends on the TranslationDictionary.json config.

            Event ev = Event.Empty();
            string translation = Event.Translate(nameof(ev.EventType));
            Assert.IsFalse(string.IsNullOrEmpty(translation));
            Assert.IsFalse(translation == nameof(ev.EventType));
            Assert.IsTrue(translation == Translator.TranslationsDictionary[$"{nameof(Event)}_{nameof(ev.EventType)}"]);
        }

        [TestMethod()]
        public void AllTest()
        {
            var result = Event.All();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }

        [TestMethod()]
        public void ContainingTest()
        {
            // 1 - try to fetch objects without stating searched text
            var result = Event.Containing("");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.BadRequest);

            // 2 - try to fetch objects with specific searched text
            result = Event.Containing("1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Item1.Any());
            Assert.IsTrue(result.Item2 == ErrorResponse.Success);
        }
    }
}