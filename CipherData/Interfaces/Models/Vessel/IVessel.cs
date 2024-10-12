namespace CipherData.Interfaces
{
    public interface IVessel : IResource
    {
        /// <summary>
        /// Packages within the vessel
        /// </summary>
        List<IPackage>? ContainingPackages { get; set; }

        /// <summary>
        /// Name of vessel
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// System in which vessel is at
        /// </summary>
        IStorageSystem System { get; set; }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        string? Type { get; set; }

        public new Dictionary<string, object?> ToDictionary()
            => new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Type)] = Type,
                [nameof(System)] = System.Name,
                [nameof(ContainingPackages)] = ContainingPackages is null ? null : string.Join(", ", ContainingPackages.Select(x => x.Id)),
            };

        /// <summary>
        /// Transfrom package object to a VesselRequest object
        /// </summary>
        /// <returns></returns>
        public IVesselRequest Request() => new VesselRequest() { Name = Name, Type = Type, SystemId = System.Id };

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single vessel given vessel ID
        /// </summary>
        /// <param name="id">vessel ID</param>
        /// <returns></returns>
        public static async Task<Tuple<IVessel, ErrorResponse>> Get(string id) => await Config.VesselsRequests.GetVessel(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static async Task<Tuple<List<IVessel>, ErrorResponse>> All() => await Config.VesselsRequests.GetVessels();

        /// <summary>
        /// Fetch all vessels which contain the searched text
        /// </summary>
        public static async Task<Tuple<List<IVessel>, ErrorResponse>> Containing(string SearchText)
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

    }
}
