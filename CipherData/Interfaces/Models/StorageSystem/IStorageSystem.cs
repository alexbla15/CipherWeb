namespace CipherData.Interfaces
{
    public interface IStorageSystem : IResource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Name of the system
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        List<IStorageSystem>? Children { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        IStorageSystem? Parent { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        Dictionary<string, string>? Properties { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>s
        IUnit Unit { get; set; }

        // STATIC METHODS

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Description)] = Description,
                [nameof(Parent)] = Parent?.Name,
                [nameof(Children)] = Children != null ? string.Join("; ", Children.Select(x => x.Name)) : null,
                [nameof(Unit)] = Unit?.Name,
                [nameof(Properties)] = Properties != null ? string.Join(", ", Properties.Select(x => $"{x.Key} : {x.Value}")) : null,
            };
        }

        // API RELATED FUNCTIONS

        /// <summary>
        /// Get details about a system vessel given system ID
        /// </summary>
        /// <param name="id">system ID</param>
        public static async Task<Tuple<IStorageSystem, ErrorResponse>> Get(string id) => await Config.SystemsRequests.GetSystem(id);

        /// <summary>
        /// All systems that took place in a certain system
        /// </summary>
        public static async Task<Tuple<List<IStorageSystem>, ErrorResponse>> All() => await Config.SystemsRequests.GetSystems();

        /// <summary>
        /// Fetch all systems which contain the searched text
        /// </summary>
        public static async Task<Tuple<List<IStorageSystem>, ErrorResponse>> Containing(string SearchText)
        {
            var result = await GetObjects<StorageSystem>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{nameof(System)}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{nameof(System)}.{nameof(Name)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Description)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Properties)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Parent)}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Children)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any },
                new() {Attribute = $"{nameof(System)}.{nameof(Unit)}.{nameof(Id)}", Value = SearchText }
            },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IStorageSystem).ToList(), result.Item2);
        }

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public async Task<Tuple<List<IEvent>, ErrorResponse>> Events() => await Events(Id);

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public async Task<Tuple<List<IProcess>, ErrorResponse>> Processes() => await Processes(Id);

        /// <summary>
        /// All packages that took place in this system
        /// </summary>
        public async Task<Tuple<List<IPackage>, ErrorResponse>> Packages() => await Packages(Id);

        /// <summary>
        /// All vessels that took place in this system
        /// </summary>
        public async Task<Tuple<List<IVessel>, ErrorResponse>> Vessels() => await Vessels(Id);

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        public static async Task<Tuple<List<IEvent>, ErrorResponse>> Events(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IEvent>(), ErrorResponse.BadRequest);

            var result = await GetObjects<RandomEvent>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Event)}.{nameof(Event.FinalStatePackages)}.{nameof(Package.System)}.{nameof(Id)}", AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
        }

        /// <summary>
        /// All processes that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        /// <returns></returns>
        public static async Task<Tuple<List<IProcess>, ErrorResponse>> Processes(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IProcess>(), ErrorResponse.BadRequest);

            var result = await GetObjects<RandomProcess>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Process)}.{nameof(Process.Events)}.{nameof(Event.FinalStatePackages)}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IProcess).ToList(), result.Item2);
        }

        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        public static async Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IPackage>(), ErrorResponse.BadRequest);

            var result = await GetObjects<RandomPackage>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Package).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IPackage).ToList(), result.Item2);
        }

        /// <summary>
        /// All vessels that are under this system
        /// </summary>
        public static async Task<Tuple<List<IVessel>, ErrorResponse>> Vessels(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IVessel>(), ErrorResponse.BadRequest);

            var result = await GetObjects<RandomVessel>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IVessel).ToList(), result.Item2);
        }
    }
}
