namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(Vessel))]
    public class Vessel : Resource, IVessel
    {
        private string? _Name;
        private string? _Type = string.Empty;

        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        public string? Type { get => _Type; set => _Type = value?.Trim(); }

        public List<IPackage>? ContainingPackages { get; set; }

        public IStorageSystem? System { get; set; }

        // API RELATED METHODS

        public async Task<Tuple<IVessel, ErrorResponse>> Get(string? id) =>
            await new VesselsRequests().GetVessel(id);

        public async Task<Tuple<List<IVessel>, ErrorResponse>> All() =>
            await new VesselsRequests().GetVessels();

        public async Task<Tuple<List<IVessel>, ErrorResponse>> Containing(string? SearchText)
        {
            var result = await GetObjects<Vessel>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Id)}", Value= SearchText },
                new() { Attribute = $"{typeof(Vessel).Name}.{nameof(Name)}", Value= SearchText },
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Type)}", Value= SearchText },
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(System)}.{nameof(Id)}", Value= SearchText },
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(ContainingPackages)}.{nameof(Id)}", Value= SearchText, Operator=Operator.Any }
                    },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IVessel).ToList(), result.Item2);
        }

        public async Task<Tuple<IVessel, ErrorResponse>> Create(IVesselRequest req) =>
            await new VesselsRequests().CreateVessel(req);

        public async Task<Tuple<IVessel, ErrorResponse>> Update(string id, IVesselRequest req)
            => await new VesselsRequests().UpdateVessel(id, req);
    }
}
