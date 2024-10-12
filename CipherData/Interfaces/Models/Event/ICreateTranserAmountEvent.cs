namespace CipherData.Interfaces
{
    public interface ICreateTranserAmountEvent
    {
        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        IPackage? AcceptingPackage { get; set; }

        /// <summary>
        /// Package that loses mass.
        /// </summary>
        IPackage? DonatingPackage { get; set; }

        /// <summary>
        /// Package that accepts mass.
        /// </summary>
        decimal Amount { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        string? Comments { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        string? ProcessId { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        string? Worker { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDonatingPackage()
        {
            CheckField result = CheckField.Required(DonatingPackage, CreateTranserAmountEvent.Translate(nameof(DonatingPackage)));
            result = result.Succeeded ? CheckField.LowerEqual(Amount, DonatingPackage.BrutMass,
            CreateTranserAmountEvent.Translate(nameof(Amount))) : result;
            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAcceptingPackage() =>
            CheckField.Required(AcceptingPackage, CreateTranserAmountEvent.Translate(nameof(AcceptingPackage)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckAmount() =>
            CheckField.Greater(Amount, 0, CreateTranserAmountEvent.Translate(nameof(Amount)));

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckDonatingDifferentFromAccepting()
            => CheckField.NotEq(AcceptingPackage?.Id, DonatingPackage?.Id,
                CreateTranserAmountEvent.Translate(nameof(AcceptingPackage)));

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
