using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.Models.Tests
{
    [TestClass()]
    public class CreateTranserAmountEventTests
    {
        private static readonly Package p1 = Package.Random("1");
        private static readonly Package p2 = Package.Random("2");
        private static readonly CreateTranserAmountEvent ev = new(worker: "אבי", timestamp: DateTime.Now, donatingPackage: p1, acceptingPackage: p2,
                comments: "c", amount: 0.1M);

        [TestMethod()]
        public void CreateTranserAmountEventTest()
        {
            Assert.IsNotNull(ev.Worker);
            Assert.IsNotNull(ev.Comments);
        }

        [TestMethod()]
        public void CheckDonatingPackageTest()
        {
            // 1 - regualr request
            p1.BrutMass = 0.5M;
            Assert.IsTrue(ev.CheckDonatingPackage().Succeeded);

            // 2 - donating package is missing
            CreateTranserAmountEvent ev_copy = ev.Copy();
            ev_copy.DonatingPackage = null;
            Assert.IsFalse(ev_copy.CheckDonatingPackage().Succeeded);

            // 3 - donating package mass is not enough missing
            ev_copy = ev.Copy();
            ev_copy.Amount = 100M;
            Assert.IsFalse(ev_copy.CheckDonatingPackage().Succeeded);
        }

        [TestMethod()]
        public void CheckAcceptingPackageTest()
        {
            // 1 - regualr request
            Assert.IsTrue(ev.CheckAcceptingPackage().Succeeded);

            // 2 - accepting package is missing
            CreateTranserAmountEvent ev_copy = ev.Copy();
            ev_copy.AcceptingPackage = null;
            Assert.IsFalse(ev_copy.CheckAcceptingPackage().Succeeded);
        }

        [TestMethod()]
        public void CheckAmountTest()
        {
            // 1 - amount > 0
            Assert.IsTrue(ev.CheckAmount().Succeeded);

            // 2 - amount <= 0
            CreateTranserAmountEvent ev_copy = ev.Copy();
            ev_copy.Amount = 0;
            Assert.IsFalse(ev_copy.CheckAmount().Succeeded);
            ev_copy.Amount = -1;
            Assert.IsFalse(ev_copy.CheckAmount().Succeeded);
        }

        [TestMethod()]
        public void CheckDonatingDifferentFromAcceptingTest()
        {
            // 1 - accepting != donating
            Assert.IsTrue(ev.CheckDonatingDifferentFromAccepting().Succeeded);

            // 2 - accepting = donating
            CreateTranserAmountEvent ev_copy = ev.Copy();
            ev_copy.AcceptingPackage = p1;
            Assert.IsFalse(ev_copy.CheckDonatingDifferentFromAccepting().Succeeded);
        }

        [TestMethod()]
        public void CheckTest()
        {
            // 1 - good fields
            p1.Id = "1";
            p2.Id = "2";
            ev.DonatingPackage = p1;
            ev.AcceptingPackage = p2;
            p1.BrutMass = 0.5M;
            Assert.IsTrue(ev.Check().Item1);

            // 2 - no donating
            CreateTranserAmountEvent ev_copy = ev.Copy();
            ev_copy.DonatingPackage = null;
            Assert.IsFalse(ev_copy.Check().Item1);

            // 3 - no accepting target
            ev_copy = ev.Copy();
            ev_copy.AcceptingPackage = null;
            Assert.IsFalse(ev_copy.Check().Item1);

            // 4 - bad amount
            ev_copy = ev.Copy();
            ev_copy.Amount = 0;
            Assert.IsFalse(ev_copy.Check().Item1);

            // 5 - accepting = donating
            ev_copy = ev.Copy();
            ev_copy.AcceptingPackage = ev.DonatingPackage;
            Assert.IsFalse(ev_copy.Check().Item1);

            // 6 - not enough mass
            ev_copy = ev.Copy();
            ev_copy.DonatingPackage.BrutMass = 0.01M;
            Assert.IsFalse(ev_copy.Check().Item1);
        }

        [TestMethod()]
        public void CreateTest()
        {
            CreateEvent ev_main = ev.Create();

            Assert.IsTrue(ev.Worker == ev_main.Worker);
            Assert.IsTrue(ev.Comments == ev_main.Comments);
            Assert.IsNull(ev.ProcessId);
            Assert.IsTrue(ev_main.EventType == 23);
            Assert.IsTrue(ev.Timestamp == ev_main.Timestamp);
            Assert.IsTrue(ev_main.Actions[0].BrutMass == ev.AcceptingPackage.BrutMass);
            Assert.IsTrue(ev_main.Actions[1].BrutMass == ev.DonatingPackage.BrutMass);
        }

        [TestMethod()]
        public void CopyTest()
        {
            CreateTranserAmountEvent example_copy = ev.Copy();

            Assert.IsNotNull(example_copy);
            Assert.IsTrue(example_copy.Worker == ev.Worker);
            Assert.IsTrue(example_copy.Timestamp == ev.Timestamp);
            Assert.IsTrue(example_copy.Comments == ev.Comments);
            Assert.IsTrue(example_copy.ProcessId == ev.ProcessId);
            Assert.IsTrue(example_copy.DonatingPackage?.Equals(ev.DonatingPackage));
            Assert.IsTrue(example_copy.AcceptingPackage?.Equals(ev.AcceptingPackage));
        }

        [TestMethod()]
        public void EmptyTest()
        {
            // instanciation of an empty object scheme
            CreateTranserAmountEvent ev_copy = CreateTranserAmountEvent.Empty();

            Assert.IsTrue(string.IsNullOrEmpty(ev_copy.Comments));
            Assert.IsTrue(string.IsNullOrEmpty(ev_copy.Worker));
            Assert.IsNull(ev_copy.DonatingPackage);
            Assert.IsNull(ev_copy.AcceptingPackage);
            Assert.IsTrue(string.IsNullOrEmpty(ev.ProcessId));
        }

        [TestMethod()]
        public void TranslateTest()
        {
            // try to translate some field
            // this depends on the TranslationDictionary.json config.

            CreateTranserAmountEvent ev = CreateTranserAmountEvent.Empty();
            string translation = CreateTranserAmountEvent.Translate(nameof(ev.DonatingPackage));
            Assert.IsFalse(string.IsNullOrEmpty(translation));
            Assert.IsFalse(translation == nameof(ev.DonatingPackage));
            Assert.IsTrue(translation == Translator.TranslationsDictionary[$"{nameof(CreateTranserAmountEvent)}_{nameof(ev.DonatingPackage)}"]);
        }
    }
}