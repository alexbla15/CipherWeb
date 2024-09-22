namespace CipherData.Models
{
    /// <summary>
    /// An event of transfering mass between one package to another
    /// </summary>
    public class CreateTranserAmountEvent : CipherClass
    {
        private string? _Worker = null;

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string? Worker
        {
            get { return _Worker; }
            set { _Worker = value?.Trim(); }
        }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; } = null;

        private string? _Comments = null;

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments
        {
            get { return _Comments; }
            set { _Comments = value?.Trim(); }
        }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Package that loses mass.
        /// </summary>
        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(DonatingPackage))]
        public Package? DonatingPackage { get; set; } = null;

        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(AcceptingPackage))]
        public Package? AcceptingPackage { get; set; } = null;

        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(Amount))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDonatingPackage()
        {
            CheckField result = CheckField.Required(DonatingPackage, Translate(nameof(DonatingPackage)));
            result = (result.Succeeded) ? CheckField.LowerEqual(Amount, DonatingPackage.BrutMass, Translate(nameof(Amount))) : result;
            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAcceptingPackage()
        {
            return CheckField.Required(AcceptingPackage, Translate(nameof(AcceptingPackage)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAmount()
        {
            return CheckField.Greater(Amount, 0, Translate(nameof(Amount)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDonatingDifferentFromAccepting()
        {
            return CheckField.NotEq(AcceptingPackage?.Id, DonatingPackage?.Id, Translate(nameof(AcceptingPackage)));
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckAmount());
            result.Fields.Add(CheckDonatingPackage());
            result.Fields.Add(CheckAcceptingPackage());
            result.Fields.Add(CheckDonatingDifferentFromAccepting());
            

            Tuple<bool,string> SpecificEventCheck = result.Check();
            return (SpecificEventCheck.Item1) ? Create().Check() : SpecificEventCheck;
        }

        /// <summary>
        /// Create a copy of this object
        /// </summary>
        /// <returns></returns>
        public CreateTranserAmountEvent Copy()
        {
            return (CreateTranserAmountEvent)MemberwiseClone();
        }

        /// <summary>
        /// Create a general CreateEvent object, out of this object parameters.
        /// </summary>
        /// <returns></returns>
        public CreateEvent Create()
        {
            if (AcceptingPackage != null && DonatingPackage != null)
            {
                DonatingPackage.BrutMass -= Amount;
                DonatingPackage.NetMass = decimal.Round(DonatingPackage.BrutMass * DonatingPackage.Concentration, 2);

                AcceptingPackage.BrutMass += Amount;
                AcceptingPackage.NetMass = decimal.Round(AcceptingPackage.BrutMass * AcceptingPackage.Concentration, 2);

                return new CreateEvent()
                {
                    Worker = Worker,
                    Timestamp = Timestamp,
                    EventType = 23,
                    ProcessId = ProcessId,
                    Comments = Comments,
                    Actions = new() { AcceptingPackage.Request(), DonatingPackage.Request() }
                };
            }

            return new CreateEvent();
        }
    }
}
