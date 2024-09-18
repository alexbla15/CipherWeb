using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CreateRelocationEventTests
    {
        [TestMethod()]
        public void CreateRelocationEventTest()
        {
            CreateRelocationEvent ev = new(worker: "a", timestamp: DateTime.Now, packages: new() { Package.Random() }, 
                comments: "c", targetSystem: StorageSystem.Random());

            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.Comments);
            Assert.IsTrue(ev.Packages?.Any());
        }

        [TestMethod()]
        public void CheckPackagesTest()
        {
            // 1 - no packages
            CreateRelocationEvent ev = new(worker: "a", timestamp: DateTime.Now, packages: new(), comments: "c", targetSystem: StorageSystem.Random());
            Assert.IsFalse(ev.CheckPackages().Succeeded);

            // 2 - null
            ev.Packages = null;
            Assert.IsFalse(ev.CheckPackages().Succeeded);

            // 3 - package problem
            ev.Packages = new() { Package.Random("@") };
            Assert.IsFalse(ev.CheckPackages().Succeeded);

            // 4 - duplicity
            Package p = Package.Random();
            ev.Packages = new() { p, p };
            Assert.IsFalse(ev.CheckPackages().Succeeded);

            // 5 - can't move package to same location
            ev.Packages = new() { p };
            ev.TargetSystem = p.System;
            Assert.IsFalse(ev.CheckPackages().Succeeded);

            // 6 - all good
            ev.TargetSystem = StorageSystem.Random();
            Assert.IsFalse(ev.CheckPackages().Succeeded);
        }

        [TestMethod()]
        public void CheckTargetSystemTest()
        {
            // 1 - good event
            Package p = Package.Random();
            CreateRelocationEvent ev = new(worker: "a", timestamp: DateTime.Now, packages: new() { p}, 
                comments: "c", targetSystem: StorageSystem.Random());
            Assert.IsTrue(ev.CheckTargetSystem().Succeeded);

            // 2 - can't be without target
            ev.TargetSystem = null;
            Assert.IsFalse(ev.CheckTargetSystem().Succeeded);
        }

        [TestMethod()]
        public void CheckTargetSystemDifferentTest()
        {
            // 1 - good event
            Package p = Package.Random();
            CreateRelocationEvent ev = new(worker: "a", timestamp: DateTime.Now, packages: new() { p },
                comments: "c", targetSystem: StorageSystem.Random());
            Assert.IsTrue(ev.CheckTargetSystemDifferent().Succeeded);

            // 2 - can't move package to the same location
            ev.TargetSystem = p.System;
            Assert.IsFalse(ev.CheckTargetSystemDifferent().Succeeded);
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            Package p = Package.Random();
            p.System.Id = "2";
            CreateRelocationEvent ev_main = new(worker: "אבי", timestamp: DateTime.Now, packages: new() { p },
                comments: "c", targetSystem: StorageSystem.Random("1"));
            Assert.IsTrue(ev_main.Check().Item1);

            // 2 - bad packages
            CreateRelocationEvent ev = ev_main.Copy();
            ev.Packages = new();
            Assert.IsFalse(ev.Check().Item1);

            // 3 - bad target
            ev = ev_main.Copy();
            ev.TargetSystem = null;
            Assert.IsFalse(ev.Check().Item1);

            // 4 - bad target-package
            ev = ev_main.Copy();
            ev.TargetSystem = p.System;
            Assert.IsFalse(ev.Check().Item1);
        }

        [TestMethod()]
        public void CreateTest()
        { 
            Package p = Package.Random();
            p.System.Id = "2";
            CreateRelocationEvent ev_main = new(worker: "אבי", timestamp: DateTime.Now, packages: new() { p },
                comments: "c", targetSystem: StorageSystem.Random("1"));

            CreateEvent ev = ev_main.Create(true);

            Assert.IsTrue(ev.Worker == ev_main.Worker);
            Assert.IsTrue(ev.Comments == ev_main.Comments);
            Assert.IsNull(ev.ProcessId);
            Assert.IsTrue(ev.EventType == 24);
            Assert.IsTrue(ev.Timestamp == ev_main.Timestamp);
            Assert.IsTrue(ev.Actions.Select(x => x.BrutMass).SequenceEqual(ev_main.Packages.Select(x => x.BrutMass).ToList()));
        }

        [TestMethod()]
        public void CopyTest()
        {
            CreateRelocationEvent ev_main = new(worker: "אבי", timestamp: DateTime.Now, packages: new() { Package.Random() },
                comments: "c", targetSystem: StorageSystem.Random());
            CreateRelocationEvent example_copy = ev_main.Copy();

            Assert.IsNotNull(example_copy);
            Assert.IsTrue(example_copy.Worker == ev_main.Worker);
            Assert.IsTrue(example_copy.Timestamp == ev_main.Timestamp);
            Assert.IsTrue(example_copy.Comments == ev_main.Comments);
            Assert.IsTrue(example_copy.TargetSystem?.Equals(ev_main.TargetSystem));
            Assert.IsTrue(example_copy.Packages?.SequenceEqual(ev_main.Packages));
        }

        [TestMethod()]
        public void ChangeLocationsTest()
        {
            Package p1 = Package.Random();
            Package p2 = Package.Random();
            StorageSystem s = StorageSystem.Random();
            CreateRelocationEvent ev_main = new(worker: "אבי", timestamp: DateTime.Now, packages: new() { p1, p2 },
                comments: "c", targetSystem: s);

            Assert.IsFalse(p1.System.Id == s.Id);
            Assert.IsFalse(p2.System.Id == s.Id);

            ev_main.ChangeLocations();

            Assert.IsTrue(p1.System.Id == s.Id);
            Assert.IsTrue(p2.System.Id == s.Id);
        }

        [TestMethod()]
        public void EmptyTest()
        {
            // instanciation of an empty object scheme
            CreateRelocationEvent ev = CreateRelocationEvent.Empty();

            Assert.IsTrue(string.IsNullOrEmpty(ev.Comments));
            Assert.IsTrue(string.IsNullOrEmpty(ev.Worker));
            Assert.IsNull(ev.Packages);
            Assert.IsNull(ev.TargetSystem);
        }

        [TestMethod()]
        public void TranslateTest()
        {
            // try to translate some field
            // this depends on the TranslationDictionary.json config.

            CreateRelocationEvent ev = CreateRelocationEvent.Empty();
            string translation = CreateRelocationEvent.Translate(nameof(ev.TargetSystem));
            Assert.IsFalse(string.IsNullOrEmpty(translation));
            Assert.IsFalse(translation == nameof(ev.TargetSystem));
            Assert.IsTrue(translation == Translator.TranslationsDictionary[$"{nameof(CreateRelocationEvent)}_{nameof(ev.TargetSystem)}"]);
        }
    }
}