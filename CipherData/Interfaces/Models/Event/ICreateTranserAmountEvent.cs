using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// An event of transfering mass between one package to another
    /// </summary>
    [HebrewTranslation(nameof(ICreateTranserAmountEvent))]
    public interface ICreateTranserAmountEvent : ICipherClass
    {
        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        [HebrewTranslation(typeof(ICreateTranserAmountEvent), nameof(AcceptingPackage))]
        IPackage? AcceptingPackage { get; set; }

        /// <summary>
        /// Package that loses mass.
        /// </summary>
        [HebrewTranslation(typeof(ICreateTranserAmountEvent), nameof(DonatingPackage))]
        IPackage? DonatingPackage { get; set; }

        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        [HebrewTranslation(typeof(ICreateTranserAmountEvent), nameof(Amount))]
        decimal Amount { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Comments))]
        string? Comments { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.ProcessId))]
        string? ProcessId { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Worker))]
        string? Worker { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Timestamp))]
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDonatingPackage()
        {
            CheckField result = CheckField.Required(DonatingPackage, Translate(nameof(DonatingPackage)));
            result = result.Succeeded ? CheckField.LowerEqual(Amount, DonatingPackage?.BrutMass ?? 0,
            Translate(nameof(Amount))) : result;
            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAcceptingPackage() =>
            CheckField.Required(AcceptingPackage, Translate(nameof(AcceptingPackage)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAmount() =>
            CheckField.Greater(Amount, 0, Translate(nameof(Amount)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDonatingDifferentFromAccepting()
            => CheckField.NotEq(AcceptingPackage?.Id, DonatingPackage?.Id,
                Translate(nameof(AcceptingPackage)));

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckAmount());
            result.Fields.Add(CheckDonatingPackage());
            result.Fields.Add(CheckAcceptingPackage());
            result.Fields.Add(CheckDonatingDifferentFromAccepting());


            Tuple<bool, string> SpecificEventCheck = result.Check();
            return SpecificEventCheck.Item1 ? Create().Check() : SpecificEventCheck;
        }

        /// <summary>
        /// Create a general CreateEvent object, out of this object parameters.
        /// </summary>
        public ICreateEvent Create()
        {
            var ev = new CreateEvent
            {
                Worker = Worker,
                Timestamp = Timestamp,
                EventType = 23,
                ProcessId = ProcessId,
                Comments = Comments,
            };

            if (AcceptingPackage != null && DonatingPackage != null)
            {
                var AccPack = Copy(AcceptingPackage);
                var DonPack = Copy(DonatingPackage);

                if (AccPack != null && DonPack != null)
                {
                    DonPack.BrutMass -= Amount;
                    DonPack.NetMass = decimal.Round(DonPack.BrutMass * DonPack.Concentration, 2);

                    AccPack.BrutMass += Amount;
                    AccPack.NetMass = decimal.Round(AccPack.BrutMass * AccPack.Concentration, 2);

                    ev.Actions = new() { AccPack.Request(), DonPack.Request() };
                }
            }

            return ev;
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
