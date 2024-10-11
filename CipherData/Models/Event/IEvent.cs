using CipherData.Models.Randomizers;

namespace CipherData.Models
{
    public interface IDisplayedEvent : ICipherClass
    {
        /// <summary>
        /// accepting package, item 1 - previous status, item 2 - final status
        /// </summary>
        Tuple<IPackage, IPackage>? AcceptingPackage { get; set; }

        /// <summary>
        /// donating package, item 1 - previous status, item 2 - final status
        /// </summary>
        Tuple<IPackage, IPackage>? DonatingPackage { get; set; }

        /// <summary>
        /// package accepting system
        /// </summary>
        IStorageSystem? AcceptingSystem { get; set; }

        /// <summary>
        /// package donating system
        /// </summary>
        IStorageSystem? DonatingSystem { get; set; }
        string? Id { get; set; }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        string? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        string? Comments { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        string? Worker { get; set; }

        /// <summary>
        /// mass transfered in the event
        /// </summary>
        decimal? EventMass { get; set; }

        /// <summary>
        /// Type of event
        /// </summary>
        int EventType { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        DateTime Timestamp { get; set; }
    }

    public interface IEvent : IResource
    {
        /// <summary>
        /// Type of event
        /// </summary>
        int EventType { get; set; }

        /// <summary>
        /// Validation status of event.
        /// </summary>
        int Status { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        string? Worker { get; set; }

        /// <summary>
        /// Process ID of process containing to this event
        /// </summary>
        string? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        string? Comments { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package before the event
        /// </summary>
        List<IPackage> InitialStatePackages { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        List<IPackage> FinalStatePackages { get; set; }

        /// <summary>
        /// Timestamp when the event happend
        /// </summary>
        DateTime Timestamp { get; set; }

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(EventType)] = EventType,
                [nameof(ProcessId)] = ProcessId,
                ["Packages"] = (FinalStatePackages != null) ? string.Join("; ", FinalStatePackages.Select(x => x.Id)) : null,
                [nameof(Status)] = Status == 0 ? "מחכה לאישור" : (Status > 0 ? "תנועה מאושרת" : "תנועה נדחתה"),
                [nameof(Timestamp)] = Timestamp,
                [nameof(Worker)] = Worker,
                [nameof(Comments)] = Comments,
            };
        }

        public bool IsRelocationEvent() =>
            InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.System.Id != fPack.System.Id).All(match => match);

        public bool IsTransferAmountEvent() =>
            InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) => iPack.BrutMass != fPack.BrutMass).All(match => match);

        public List<IDisplayedEvent> GetRelocationEvents()
        {
            return InitialStatePackages.Zip(FinalStatePackages,
                (iPack, fPack) =>
                new DisplayedEvent()
                {
                    Id = Id,
                    EventType = EventType,
                    Worker = Worker,
                    Timestamp = Timestamp,
                    ProcessId = ProcessId,
                    Comments = Comments,
                    DonatingPackage = Tuple.Create(iPack, fPack),
                    DonatingSystem = iPack.System,
                    AcceptingSystem = fPack.System
                }).Select(x => x as IDisplayedEvent).ToList();
        }

        public List<IDisplayedEvent> GetTransferAmountEvent()
        {
            List<IDisplayedEvent> results = new();

            List<Tuple<IPackage, IPackage>> DonatingPacks = new();
            List<Tuple<IPackage, IPackage>> AcceptingPacks = new();

            for (int i = 0; i < InitialStatePackages.Count; i++)
            {
                int indexer = i;

                decimal iMass = InitialStatePackages[indexer].BrutMass;
                decimal fMass = FinalStatePackages[indexer].BrutMass;

                if (iMass > fMass) DonatingPacks.Add(Tuple.Create(InitialStatePackages[indexer], FinalStatePackages[indexer]));
                if (iMass < fMass) AcceptingPacks.Add(Tuple.Create(InitialStatePackages[indexer], FinalStatePackages[indexer]));
            }

            DisplayedEvent ev = new()
            {
                Id = Id,
                Worker = Worker,
                Status = Status,
                Comments = Comments,
                ProcessId = ProcessId,
                Timestamp = Timestamp,
                EventType = EventType
            };

            // 1 - only one donating package, as it should be
            if (DonatingPacks.Count == 1 && AcceptingPacks.Any())
            {
                foreach (Tuple<IPackage, IPackage> acceptingPack in AcceptingPacks)
                {
                    ev.DonatingPackage = DonatingPacks[0];
                    ev.AcceptingPackage = acceptingPack;
                    ev.DonatingSystem = acceptingPack.Item1.System;
                    ev.AcceptingSystem = acceptingPack.Item1.System;
                    ev.EventMass = acceptingPack.Item2.BrutMass - acceptingPack.Item1.BrutMass;
                    results.Add(ev);
                }
            }

            // 2 - many donating packages, than it is set improperly
            if (DonatingPacks.Count > 1 && AcceptingPacks.Any())
            {
                foreach (Tuple<IPackage, IPackage> acceptingPack in AcceptingPacks)
                {
                    ev.DonatingPackage = null;
                    ev.AcceptingPackage = acceptingPack;
                    ev.DonatingSystem = acceptingPack.Item1.System;
                    ev.AcceptingSystem = acceptingPack.Item1.System;
                    ev.EventMass = acceptingPack.Item2.BrutMass - acceptingPack.Item1.BrutMass;
                    results.Add(ev);
                }
                foreach (Tuple<IPackage, IPackage> donatingPack in DonatingPacks)
                {
                    ev.AcceptingPackage = null;
                    ev.DonatingPackage = donatingPack;
                    ev.DonatingSystem = donatingPack.Item1.System;
                    ev.AcceptingSystem = donatingPack.Item1.System;
                    ev.EventMass = donatingPack.Item2.BrutMass - donatingPack.Item1.BrutMass;
                    results.Add(ev);
                }
            }

            return results;
        }

        public List<IDisplayedEvent> TransformToDisplayedEvents()
        {
            if (IsRelocationEvent()) return GetRelocationEvents();
            if (IsTransferAmountEvent()) return GetTransferAmountEvent();
            return new();
        }


        // API-RELATED FUNCTIONS

        public Tuple<IEvent, ErrorResponse> Update(IUpdateEvent update_details) => Config.EventsRequests.UpdateEvent(Id, update_details);

        /// <summary>
        /// Fetch all events which contain the searched text
        /// </summary>
        public static Tuple<List<IEvent>, ErrorResponse> Containing(string? SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            var result = GetObjects<Event>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Id)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Worker)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(EventType)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(ProcessId)}", Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(Comments)}",Value = searchText },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(InitialStatePackages)}.{nameof(Id)}", Value = searchText, Operator = Operator.Any },
                new() { Attribute = $"{typeof(Event).Name}.{nameof(FinalStatePackages)}.{nameof(Id)}", Value = searchText, Operator = Operator.Any }
            },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
        }

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<IEvent>, ErrorResponse> All() => Config.EventsRequests.GetEvents();

        /// <summary>
        /// Fetch all events with specific status
        /// </summary>
        private static Tuple<List<IEvent>, ErrorResponse> StatusEvents(int status)
        {
            if (new Random().Next(2) == 0)
            {
                var result = GetObjects<RandomRelocationEvent>(status.ToString(), searchText => new GroupedBooleanCondition()
                {
                    Conditions = new List<BooleanCondition>() {
                    new() { Attribute = $"{typeof(Event).Name}.{nameof(Status)}", Value = searchText, AttributeRelation=AttributeRelation.Eq }
                    },
                    Operator = Operator.Any
                });
                return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
            }
            else
            {
                var result = GetObjects<RandomTransferAmountEvent>(status.ToString(), searchText => new GroupedBooleanCondition()
                {
                    Conditions = new List<BooleanCondition>() {
                    new() { Attribute = $"{typeof(Event).Name}.{nameof(Status)}", Value = searchText, AttributeRelation=AttributeRelation.Eq }
                    },
                    Operator = Operator.Any
                });
                return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
            }
        }

        public static Tuple<List<IEvent>, ErrorResponse> PendingEvents() => StatusEvents(0);
        public static Tuple<List<IEvent>, ErrorResponse> ApprovedEvents() => StatusEvents(1);
        public static Tuple<List<IEvent>, ErrorResponse> DeclinedEvents() => StatusEvents(-1);

    }
}
