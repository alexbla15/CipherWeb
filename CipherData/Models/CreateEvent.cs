using CipherData.Requests;
using System.Diagnostics;
using System;
using System.Xml.Linq;

namespace CipherData.Models
{
    /// <summary>
    /// Create new event
    /// </summary>
    public class CreateEvent
    {
        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string Worker { get; set; }

        /// <summary>
        /// Type of event. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.EventType))]
        public int EventType { get; set; }

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
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Packages))]
        public List<PackageRequest> Actions { get; set; }

        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="worker">Name of updating worker. Required</param>
        /// <param name="eventType">Type of event. Required</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="actions">List of affected packages from actions, the items present the state of each package after the event</param>
        public CreateEvent(string worker, DateTime? timestamp, int eventType, List<PackageRequest> actions, string? processId = null, string? comments = null)
        {
            Worker = worker;
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = actions;
        }

        /// <summary>
        /// Create new event (Use for one-package-event)
        /// </summary>
        /// <param name="worker">Name of updating worker. Required</param>
        /// <param name="eventType">Type of event. Required</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="action">affected package, the item present the state of package after the event</param>
        public CreateEvent(string worker, DateTime timestamp, int eventType, PackageRequest action, string? processId = null, string? comments = null)
        {
            Worker = worker;
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = new List<PackageRequest>() { action };
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckWorker()
        {
            return CheckField.Required(Worker, Translate(nameof(Worker)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckEventType()
        {
            return CheckField.NotEq(EventType, 0, Translate(nameof(EventType)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckComments()
        {
            return CheckField.Required(Comments, Translate(nameof(Comments)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTimeStamp()
        {
            CheckField result = CheckField.Required(Timestamp, Translate(nameof(Timestamp)));
            result = (result.Succeeded) ? CheckField.Between((DateTime)Timestamp, DateTime.Parse("01/01/1900"), DateTime.Now, Translate(nameof(Timestamp))) : result;

            return result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckActions()
        {
            CheckField result = CheckField.FullList(Actions, Translate(nameof(Actions)));
            result = (result.Succeeded) ? CheckField.ListItems(Actions, Translate(nameof(Actions))) : result;
            result = (result.Succeeded) ? CheckField.Distinct(Actions.Select(x => x.Id).ToList(), Translate(nameof(Actions))) : result;

            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckWorker());
            result.Fields.Add(CheckEventType());
            result.Fields.Add(CheckComments());
            result.Fields.Add(CheckTimeStamp());
            result.Fields.Add(CheckActions());

            return result.Check();
        }

        public Tuple<bool, string> CheckTransferAmountEvent()
        {
            Tuple<bool, string> result = Check();

            return result;
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(CreateEvent), searchedAttribute);
        }

        /// <summary>
        /// Return an empty CreateEvent object scheme.
        /// </summary>
        public static CreateEvent Empty()
        {
            return new CreateEvent(worker: string.Empty, timestamp: DateTime.Now, eventType: 0, actions: new List<PackageRequest>());
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>s
        public string ToJson()
        {
            return Resource.ToJson(this);
        }
    }

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
        public DateTime? Timestamp { get; set; }

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
        public Decimal Amount { get; set; }

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
        public CreateTranserAmountEvent(string worker, DateTime? timestamp, decimal amount,
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

    /// <summary>
    /// An event of relocating several packages to a new location
    /// </summary>
    public class CreateRelocationEvent
    {
        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Worker))]
        public string Worker { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Comments))]
        public string? Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(Event), nameof(Event.Timestamp))]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Package that relocate in this event.
        /// </summary>
        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(Packages))]
        public List<Package>? Packages { get; set; }

        /// <summary>
        /// System to which the packages are relocated.
        /// </summary>
        [HebrewTranslation(typeof(CreateRelocationEvent), nameof(TargetSystem))]
        public StorageSystem? TargetSystem { get; set; }

        /// <summary>
        /// Create an event of transfering mass between donating and accepting packages.
        /// </summary>
        /// <param name="worker">Name of updating worker. Required</param>
        /// <param name="targetSystem">system to which the packages are relocated</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <returns></returns>
        public CreateRelocationEvent(string worker, DateTime? timestamp, StorageSystem? targetSystem = null,
            List<Package>? packages = null, string? comments = null)
        {
            TargetSystem = targetSystem;
            Worker = worker;
            Timestamp = timestamp;
            Comments = comments;
            Packages = packages;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckPackages()
        {
            CheckField result = CheckField.Required(Packages, Translate(nameof(Packages)));
            return (result.Succeeded) ? CheckField.FullList(Packages, Translate(nameof(Packages))) : result;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystem()
        {
            return CheckField.Required(TargetSystem, Translate(nameof(TargetSystem)));
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckTargetSystemDifferent()
        {
            CheckField result = new();

            if (Packages != null)
            {
                foreach (Package p in Packages)
                {
                    result = (result.Succeeded) ? CheckField.NotEq(p?.System.Id, TargetSystem?.Id, Package.Translate(nameof(p.System))) : result;
                }
            }
            else
            {
                return CheckPackages();
            }

            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckPackages());
            result.Fields.Add(CheckTargetSystem());
            result.Fields.Add(CheckTargetSystemDifferent());


            Tuple<bool, string> SpecificEventCheck = result.Check();
            return (SpecificEventCheck.Item1) ? Create().Check() : SpecificEventCheck;
        }

        /// <summary>
        /// Create a general CreateEvent object, out of this object parameters.
        /// </summary>
        /// <returns></returns>
        public CreateEvent Create()
        {
            if (Packages != null && TargetSystem != null)
            {
                foreach (Package p in Packages)
                {
                    p.System = TargetSystem;
                }

                return new CreateEvent(worker: Worker, timestamp: Timestamp, eventType: 24, comments: Comments,
                    actions: Packages.Select(x=>x.Request()).ToList());
            }

            return CreateEvent.Empty();
        }

        /// <summary>
        /// Return an empty CreateEvent object scheme.
        /// </summary>
        public static CreateRelocationEvent Empty()
        {
            return new CreateRelocationEvent(worker: string.Empty, timestamp: DateTime.Now);
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(CreateRelocationEvent), searchedAttribute);
        }
    }
}
