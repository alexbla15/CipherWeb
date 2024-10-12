using CipherData.ApiMode.Models.Event;
using CipherData.ApiMode.Models.Package;
using CipherData.ApiMode.Models.StorageSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CreateRelocationEventTests
    {
        [TestMethod()]
        public void CreateRelocationEventTest()
        {
            CreateRelocationEvent ev = new()
            {
                Worker = "a",
                Timestamp = DateTime.Now,
                Packages = new() { Package.Random() },
                Comments = "c",
                TargetSystem = StorageSystem.Random()
            };

            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.Comments);
            Assert.IsTrue(ev.Packages?.Any());
        }

        [TestMethod()]
        public void CheckPackagesTest()
        {
            // 1 - no packages
            CreateRelocationEvent ev = new()
            {
                Worker = "a",
                Timestamp = DateTime.Now,
                Packages = new(),
                Comments = "c",
                TargetSystem = StorageSystem.Random()
            };
            Assert.IsFalse(ev.CheckPackages().Succeeded);

            // 2 - null
            ev.Packages = null;
            Assert.IsFalse(ev.CheckPackages().Succeeded);

            // 3 - duplicity
            Package p = Package.Random();
            ev.Packages = new() { p, p };
            Assert.IsFalse(ev.CheckPackages().Succeeded);
        }

        [TestMethod()]
        public void CheckTargetSystemTest()
        {
            // 1 - good event
            Package p = Package.Random();
            CreateRelocationEvent ev = new()
            {
                Worker = "a",
                Timestamp = DateTime.Now,
                Packages = new() { p },
                Comments = "c",
                TargetSystem = StorageSystem.Random()
            };
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
            CreateRelocationEvent ev = new()
            {
                Worker = "a",
                Timestamp = DateTime.Now,
                Packages = new() { p },
                Comments = "c",
                TargetSystem = StorageSystem.Random()
            };
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
            CreateRelocationEvent ev_main = new()
            {
                Worker = "אבי",
                Timestamp = DateTime.Now,
                Packages = new() { p },
                Comments = "c",
                TargetSystem = StorageSystem.Random("1")
            };
            Assert.IsTrue(ev_main.Check().Item1);

            // 2 - bad packages
            CreateRelocationEvent ev = CipherClass.Copy(ev_main);
            ev.Packages = new();
            Assert.IsFalse(ev.Check().Item1);

            // 3 - bad target
            ev = CipherClass.Copy(ev_main);
            ev.TargetSystem = null;
            Assert.IsFalse(ev.Check().Item1);

            // 4 - bad target-package
            ev = CipherClass.Copy(ev_main);
            ev.TargetSystem = p.System;
            Assert.IsFalse(ev.Check().Item1);
        }

        [TestMethod()]
        public void CreateTest()
        { 
            Package p = Package.Random();
            p.System.Id = "2";
            CreateRelocationEvent ev_main = new()
            {
                Worker = "אבי",
                Timestamp = DateTime.Now,
                Packages = new() { p },
                Comments = "c",
                TargetSystem = StorageSystem.Random("1")
            };

            CreateEvent ev = ev_main.Create(true);

            Assert.IsTrue(ev.Worker == ev_main.Worker);
            Assert.IsTrue(ev.Comments == ev_main.Comments);
            Assert.IsNull(ev.ProcessId);
            Assert.IsTrue(ev.EventType == 24);
            Assert.IsTrue(ev.Timestamp == ev_main.Timestamp);
            Assert.IsTrue(ev.Actions.Select(x => x.BrutMass).SequenceEqual(ev_main.Packages.Select(x => x.BrutMass).ToList()));
        }

        [TestMethod()]
        public void ChangeLocationsTest()
        {
            Package p1 = Package.Random();
            Package p2 = Package.Random();
            StorageSystem s = StorageSystem.Random();
            CreateRelocationEvent ev_main = new()
            {
                Worker = "אבי",
                Timestamp = DateTime.Now,
                Packages = new() { p1, p2 },
                Comments = "c",
                TargetSystem = s
            };

            Assert.IsFalse(p1.System.Id == s.Id);
            Assert.IsFalse(p2.System.Id == s.Id);

            ev_main.ChangeLocations();

            Assert.IsTrue(p1.System.Id == s.Id);
            Assert.IsTrue(p2.System.Id == s.Id);
        }
    }
}