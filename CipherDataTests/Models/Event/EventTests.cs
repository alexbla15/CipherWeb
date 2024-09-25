using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class EventTests
    {
        private static readonly Event example = new()
        {
            Worker = "א",
            Timestamp = DateTime.Now,
            FinalStatePackages = new() { Package.Random() },
            ProcessId = "b",
            Comments = "c",
            EventType = 1,
            Status = 0
        }; 

        [TestMethod()]
        public void EventTest()
        {
            // 1 - without stating id
            Assert.IsNotNull(example.Id);

            // 2 - with stating id
            Event ev = new()
            {
                Id = "1",
                Worker = "א",
                Timestamp = DateTime.Now,
                FinalStatePackages = new() { Package.Random() },
                ProcessId = "b",
                Comments = "c",
                EventType = 1,
                Status = 0
            };

            Assert.IsNotNull(ev.Id);
        }

        [TestMethod()]
        public void GetNextIdTest()
        {
            Assert.IsFalse(string.IsNullOrEmpty(Event.GetNextId()));
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