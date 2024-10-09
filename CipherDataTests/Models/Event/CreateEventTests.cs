using CipherData.Models.Randomizers;
using CipherData.Randomizer;
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
            Actions = new() { new RandomPackage().Request() },
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
                Actions = new() { new RandomPackage().Request() },
                ProcessId = "b",
                Comments = "c",
                EventType = 1
            };

            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.ProcessId);
            Assert.IsNotNull(ev.Comments);

            // 2 - more than one action
            ev.Actions = RandomData.GetRandomPackages(3).Select(x=>x.Request()).ToList();

            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.ProcessId);
            Assert.IsNotNull(ev.Comments);
            Assert.IsTrue(ev.Actions.Any());
        }

        [TestMethod()]
        public void CheckWorkerTest()
        {
            CreateEvent ev = CipherClass.Copy(example);

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
            CreateEvent ev = CipherClass.Copy(example);

            ev.EventType = 12; // Good value
            Assert.IsTrue(ev.CheckEventType().Succeeded);

            ev.EventType = 0; // bad value
            Assert.IsFalse(ev.CheckEventType().Succeeded);
        }

        [TestMethod()]
        public void CheckCommentsTest()
        {
            CreateEvent ev = CipherClass.Copy(example);

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
            CreateEvent ev = new() { ProcessId = "תכונה" }; // Good name
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
            CreateEvent ev = CipherClass.Copy(example);

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
            CreateEvent ev = CipherClass.Copy(example);

            Assert.IsTrue(ev.CheckActions().Succeeded); // full list

            ev.Actions = new(); // empty list
            Assert.IsFalse(ev.CheckActions().Succeeded);

            ev.Actions = new() { new PackageRequest()}; // bad request
            Assert.IsFalse(ev.CheckActions().Succeeded);

            PackageRequest p = new RandomPackage().Request();
            ev.Actions = new() { p, p }; // bad request
            Assert.IsFalse(ev.CheckActions().Succeeded);
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            CreateEvent ev = CipherClass.Copy(example);
            Assert.IsTrue(ev.Check().Item1);

            // 2 - bad worker
            ev.Worker = "@";
            Assert.IsFalse(ev.Check().Item1);

            // 3 - bad event type
            ev = CipherClass.Copy(example);
            ev.EventType = 0;
            Assert.IsFalse(ev.Check().Item1);

            // 4 - bad Process Id
            ev = CipherClass.Copy(example);
            ev.ProcessId = "@";
            Assert.IsFalse(ev.Check().Item1);

            // 5 - bad comments
            ev = CipherClass.Copy(example);
            ev.Comments = "@";
            Assert.IsFalse(ev.Check().Item1);

            // 6 - bad timestamp
            ev = CipherClass.Copy(example);
            ev.Timestamp = DateTime.Now.AddYears(1);
            Assert.IsFalse(ev.Check().Item1);

            // 7 - bad actions
            ev = CipherClass.Copy(example);
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
            Assert.IsTrue(ev.FinalStatePackages.Select(x=>x.BrutMass).SequenceEqual(example.Actions.Select(x => x.BrutMass).ToList()));
        }
    }
}