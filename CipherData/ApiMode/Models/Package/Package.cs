namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(Package))]
    public class Package : Resource, IPackage
    {
        private string? _Description;
        private ICategory _Category = new Category();

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public List<IPackageProperty>? Properties { get; set; }

        public IVessel? Vessel { get; set; }

        public IStorageSystem System { get; set; } = new StorageSystem();

        public decimal BrutMass { get; set; }

        public decimal NetMass { get; set; }

        public DateTime CreatedAt { get; set; }

        public IPackage? Parent { get; set; }

        public List<IPackage>? Children { get; set; }

        public ICategory Category
        {
            get => _Category;
            set
            {
                _Category = value;
                DestinationProcesses = value.ConsumingProcesses;

                Properties = value.Properties?
                .DistinctBy(prop => prop.Name)
                .Select(prop => new PackageProperty { Name = prop.Name ?? string.Empty, Value = prop.DefaultValue }
                as IPackageProperty)
                .ToList();
            }
        }

        public List<IProcessDefinition> DestinationProcesses { get; set; } = new();

        public decimal Concentration => BrutMass > 0 ? NetMass / BrutMass : 0;

        // API METHODS

        public async Task<Tuple<List<IPackage>, ErrorResponse>> All() => await new PackagesRequests().GetPackages();

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        public async Task<Tuple<List<IPackage>, ErrorResponse>> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) return new(new(), ErrorResponse.BadRequest);

            var result = await GetObjects<Package>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Description)}", Value = searchText },
                new() { Attribute = $"{typeof(Package).Name}.{nameof(Properties)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Vessel)}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(System)}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Children)}.{nameof(Id)}", Value = searchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IPackage).ToList(), result.Item2);
        }

        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="id">package ID</param>
        public async Task<Tuple<IPackage, ErrorResponse>> Get(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return await Task.FromResult(Tuple.Create(new Package() as IPackage, ErrorResponse.BadRequest));
            return await new PackagesRequests().GetPackage(id);
        }

        public async Task<Tuple<IPackage, ErrorResponse>> Update(string id, IUpdatePackage req)
            => await new PackagesRequests().UpdatePackage(id, req);

        public async Task<Tuple<List<IEvent>, ErrorResponse>> Events()
        {
            if (string.IsNullOrEmpty(System?.Id)) return Tuple.Create(new List<IEvent>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Event>(System.Id, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Event)}.{nameof(Event.FinalStatePackages)}.{nameof(System)}.{nameof(Id)}", 
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
        }

        public async Task<Tuple<List<IProcess>, ErrorResponse>> Processes()
        {
            if (string.IsNullOrEmpty(System?.Id)) return Tuple.Create(new List<IProcess>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Process>(System.Id, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Process)}.{nameof(Process.Events)}.{nameof(Event.FinalStatePackages)}.{nameof(System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IProcess).ToList(), result.Item2);
        }

    }
}
