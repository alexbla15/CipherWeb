using System.Reflection;

namespace CipherData.Models
{
    public interface IVessel: IResource
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

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Type)] = Type,
                [nameof(System)] = System.Name,
                [nameof(ContainingPackages)] = ContainingPackages is null ? null : string.Join(", ", ContainingPackages.Select(x => x.Id)),
            };
        }
    }

    [HebrewTranslation(nameof(Vessel))]
    public class Vessel : Resource, IVessel
    {
        private string? _Name;
        private string? _Type = string.Empty;

        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string? Type { get => _Type; set => _Type = value?.Trim(); }

        [HebrewTranslation(typeof(Vessel), nameof(ContainingPackages))]
        public List<IPackage>? ContainingPackages { get; set; }

        [HebrewTranslation(typeof(Vessel), nameof(System))]
        public IStorageSystem System { get; set; } = new StorageSystem();

        /// <summary>
        /// Transfrom package object to a VesselRequest object
        /// </summary>
        /// <returns></returns>
        public VesselRequest Request() => new() { Name = Name, Type = Type, SystemId = System.Id };

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single vessel given vessel ID
        /// </summary>
        /// <param name="id">vessel ID</param>
        /// <returns></returns>
        public static Tuple<IVessel, ErrorResponse> Get(string id) => Config.VesselsRequests.GetVessel(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<IVessel>, ErrorResponse> All() => Config.VesselsRequests.GetVessels();

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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod().DeclaringType, text);
    }
}
