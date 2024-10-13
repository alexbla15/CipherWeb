using System.Reflection;

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


        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single vessel given vessel ID
        /// </summary>
        /// <param name="id">vessel ID</param>
        Task<Tuple<IVessel, ErrorResponse>> Get(string id);

        /// <summary>
        /// All objects
        /// </summary>
        Task<Tuple<List<IVessel>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all vessels which contain the searched text
        /// </summary>
        Task<Tuple<List<IVessel>, ErrorResponse>> Containing(string SearchText);

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<IVessel, ErrorResponse>> Create(IVesselRequest req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IVessel, ErrorResponse>> Update(string id, IVesselRequest req);
    }
}
