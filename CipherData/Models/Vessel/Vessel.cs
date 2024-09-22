using CipherData.Requests;

namespace CipherData.Models
{
    public class Vessel : Resource
    {
        private string? _Name = null;

        /// <summary>
        /// Name of vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get { return _Name; } set { _Name = value?.Trim(); } }

        private string _Type = string.Empty;

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string Type { get { return _Type; } set { _Type = value.Trim(); } }

        /// <summary>
        /// Packages within the vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(ContainingPackages))]
        public List<Package>? ContainingPackages { get; set; } = null;

        /// <summary>
        /// System in which vessel is at
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(System))]
        public StorageSystem System { get; set; } = new();

        /// <summary>
        /// Vessel containing some packages, inside some system
        /// </summary>
        public Vessel(string? id = null)
        {
            string nextId = GetNextId();

            Id = id ?? nextId;
            Name ??= nextId;
        }

        /// <summary>
        /// Transfrom package object to a VesselRequest object
        /// </summary>
        /// <returns></returns>
        public VesselRequest Request()
        {
            return new VesselRequest()
            { Name = Name, Type = Type, SystemId = System.Id };
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId()
        {
            IdCounter += 1;
            return $"V{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<Vessel>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Vessel Random(string? id = null)
        {
            List<string> VesselTypes = new() { "קופסה", "ארגז", "צנצנת" };

            return new Vessel(id)
            {
                Type = RandomFuncs.RandomItem(VesselTypes),
                System = StorageSystem.Random(),
                ContainingPackages = new List<Package>() { RandomData.RandomPackage }
            };
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(Vessel), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single vessel given vessel ID
        /// </summary>
        /// <param name="id">vessel ID</param>
        /// <returns></returns>
        public static Tuple<Vessel, ErrorResponse> Get(string id)
        {
            return VesselsRequests.GetVessel(id);
        }

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Vessel>, ErrorResponse> All()
        {
            return VesselsRequests.GetVessels();
        }

        /// <summary>
        /// Fetch all vessels which contain the searched text
        /// </summary>
        public static Tuple<List<Vessel>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Vessel>(SearchText, searchText => new GroupedBooleanCondition()
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
        }
    }
}
