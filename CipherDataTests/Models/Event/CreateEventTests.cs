using CipherData.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CreateEventTests
    {
        private static readonly CreateEvent example = new()
        {
            Worker = "א",
            Timestamp = DateTime.Now,
            Actions = new() { PackageRequest.Random() },
            ProcessId = "b",
            Comments = "c",
            EventType = 1
        };

        [TestMethod()]
        public void CreateEventTest()
        {
            // 1 - only one action
            CreateEvent ev = new()
            {
                Worker = "a",
                Timestamp = DateTime.Now,
                Actions = new() { PackageRequest.Random() },
                ProcessId = "b",
                Comments = "c",
                EventType = 1
            };

            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.ProcessId);
            Assert.IsNotNull(ev.Comments);

            // 2 - more than one action
            ev.Actions = RandomFuncs.FillRandomObjects(3, PackageRequest.Random);

            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.ProcessId);
            Assert.IsNotNull(ev.Comments);
            Assert.IsTrue(ev.Actions.Any());
        }

        [TestMethod()]
        public void CopyTest()
        {
            CreateEvent example_copy = example.Copy();

            Assert.IsNotNull(example_copy);
            Assert.IsTrue(example_copy.Worker == example.Worker);
            Assert.IsTrue(example_copy.Timestamp == example.Timestamp);
            Assert.IsTrue(example_copy.ProcessId == example.ProcessId);
            Assert.IsTrue(example_copy.Comments == example.Comments);
            Assert.IsTrue(example_copy.EventType == example.EventType);
            Assert.IsTrue(example_copy.Actions.SequenceEqual(example.Actions));
        }

        [TestMethod()]
        public void CheckWorkerTest()
        {
            CreateEvent ev = example.Copy();

            ev.Worker = "תכונה"; // Good name
            Assert.IsTrue(ev.CheckWorker().Succeeded);

            ev.Worker = "@תכונה"; // Improper chars
            Assert.IsFalse(ev.CheckWorker().Succeeded);

            ev.Worker = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(ev.CheckWorker().Succeeded);

            ev.Worker = ""; ; // Empty
            Assert.IsFalse(ev.CheckWorker().Succeeded);

            ev.Worker = null; ; // Null
            Assert.IsFalse(ev.CheckWorker().Succeeded);
        }

        [TestMethod()]
        public void CheckEventTypeTest()
        {
            CreateEvent ev = example.Copy();

            ev.EventType = 12; // Good value
            Assert.IsTrue(ev.CheckEventType().Succeeded);

            ev.EventType = 0; // bad value
            Assert.IsFalse(ev.CheckEventType().Succeeded);
        }

        [TestMethod()]
        public void CheckCommentsTest()
        {
            CreateEvent ev = example.Copy();

            ev.Comments = "תכונה"; // Good name
            Assert.IsTrue(ev.CheckComments().Succeeded);

            ev.Comments = "@תכונה"; // Improper chars
            Assert.IsFalse(ev.CheckComments().Succeeded);

            ev.Comments = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(ev.CheckComments().Succeeded);

            ev.Comments = ""; ; // Empty
            Assert.IsFalse(ev.CheckComments().Succeeded);

            ev.Comments = null; ; // Null
            Assert.IsFalse(ev.CheckComments().Succeeded);
        }

        [TestMethod()]
        public void CheckProcessIdTest()
        {
            CreateEvent ev = new()
            {
                ProcessId = "תכונה" // Good name
            };
            Assert.IsTrue(ev.CheckProcessId().Succeeded);

            ev.ProcessId = "@תכונה"; // Improper chars
            Assert.IsFalse(ev.CheckProcessId().Succeeded);

            ev.ProcessId = "SELectתכונה"; ; // Improper words
            Assert.IsFalse(ev.CheckProcessId().Succeeded);

            ev.ProcessId = ""; ; // Empty
            Assert.IsTrue(ev.CheckProcessId().Succeeded);

            ev.ProcessId = null; ; // Null
            Assert.IsTrue(ev.CheckProcessId().Succeeded);
        }

        [TestMethod()]
        public void CheckTimeStampTest()
        {
            CreateEvent ev = example.Copy();

            ev.Timestamp = DateTime.Now; // Good value
            Assert.IsTrue(ev.CheckTimeStamp().Succeeded);

            ev.Timestamp = DateTime.Now.AddYears(-1000); // too old
            Assert.IsFalse(ev.CheckTimeStamp().Succeeded);

            ev.Timestamp = DateTime.Now.AddYears(1); // didn't happen
            Assert.IsFalse(ev.CheckTimeStamp().Succeeded);
        }

        [TestMethod()]
        public void CheckActionsTest()
        {
            CreateEvent ev = example.Copy();

            Assert.IsTrue(ev.CheckActions().Succeeded); // full list

            ev.Actions = new(); // empty list
            Assert.IsFalse(ev.CheckActions().Succeeded);

            ev.Actions = new() { new PackageRequest()}; // bad request
            Assert.IsFalse(ev.CheckActions().Succeeded);

            PackageRequest p = PackageRequest.Random();
            ev.Actions = new() { p, p }; // bad request
            Assert.IsFalse(ev.CheckActions().Succeeded);
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            CreateEvent ev = example.Copy();
            Assert.IsTrue(ev.Check().Item1);

            // 2 - bad worker
            ev.Worker = "@";
            Assert.IsFalse(ev.Check().Item1);

            // 3 - bad event type
            ev = example.Copy();
            ev.EventType = 0;
            Assert.IsFalse(ev.Check().Item1);

            // 4 - bad Process Id
            ev = example.Copy();
            ev.ProcessId = "@";
            Assert.IsFalse(ev.Check().Item1);

            // 5 - bad comments
            ev = example.Copy();
            ev.Comments = "@";
            Assert.IsFalse(ev.Check().Item1);

            // 6 - bad timestamp
            ev = example.Copy();
            ev.Timestamp = DateTime.Now.AddYears(1);
            Assert.IsFalse(ev.Check().Item1);

            // 7 - bad actions
            ev = example.Copy();
            ev.Actions = new();
            Assert.IsFalse(ev.Check().Item1);
        }

        [TestMethod()]
        public void CreateTest()
        {
            Event ev = example.Create("1");

            Assert.IsTrue(ev.Id == "1");
            Assert.IsTrue(ev.Worker == example.Worker);
            Assert.IsTrue(ev.Comments == example.Comments);
            Assert.IsTrue(ev.ProcessId == example.ProcessId);
            Assert.IsTrue(ev.EventType == example.EventType);
            Assert.IsTrue(ev.Timestamp == example.Timestamp);
            Assert.IsTrue(ev.Packages.Select(x=>x.BrutMass).SequenceEqual(example.Actions.Select(x => x.BrutMass).ToList()));
        }
    }
}