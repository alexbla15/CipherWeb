namespace CipherData.Models
{
    /// <summary>
    /// An event of transfering mass between one package to another
    /// </summary>
    public class CreateTranserAmountEvent
    {
        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string Worker { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.ProcessId))]
        public string? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Package that loses mass.
        /// </summary>
        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(DonatingPackage))]
        public Package? DonatingPackage { get; set; }

        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(AcceptingPackage))]
        public Package? AcceptingPackage { get; set; }

        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        [HebrewTranslation(typeof(CreateTranserAmountEvent), nameof(Amount))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Create an event of transfering mass between donating and accepting packages.
        /// </summary>
        /// <param name="worker">Name of updating worker. Required</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="donatingPackage">package losing mass</param>
        /// <param name="acceptingPackage">package gaining mass</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <returns></returns>
        public CreateTranserAmountEvent(string worker, DateTime timestamp, decimal amount,
            Package? donatingPackage = null, Package? acceptingPackage = null, 
            string? processId = null, string? comments = null)
        {
            Amount = amount;
            Worker = worker;
            Timestamp = timestamp;
            ProcessId = processId;
            Comments = comments;
            DonatingPackage = donatingPackage;
            AcceptingPackage = acceptingPackage;
        }

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
            return new CreateTranserAmountEvent(Worker, Timestamp, Amount, DonatingPackage, AcceptingPackage, ProcessId, Comments);
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
                DonatingPackage.NetMass = DonatingPackage.BrutMass * DonatingPackage.Concentration;

                AcceptingPackage.BrutMass += Amount;
                AcceptingPackage.NetMass = AcceptingPackage.BrutMass * AcceptingPackage.Concentration;

                return new CreateEvent(worker: Worker, timestamp: Timestamp, eventType: 23, processId: ProcessId, comments: Comments,
                    actions: new List<PackageRequest>() { AcceptingPackage.Request(), DonatingPackage.Request() });
            }

            return CreateEvent.Empty();
        }

        /// <summary>
        /// Return an empty CreateEvent object scheme.
        /// </summary>
        public static CreateTranserAmountEvent Empty()
        {
            return new CreateTranserAmountEvent(amount: 0, worker: string.Empty, timestamp: DateTime.Now);
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(CreateTranserAmountEvent), searchedAttribute);
        }
    }
}
