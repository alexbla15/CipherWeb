﻿using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(IVessel))]
    public interface IVessel : IResource
    {
        /// <summary>
        /// Packages within the vessel
        /// </summary>
        [HebrewTranslation(typeof(IVessel), nameof(ContainingPackages))]
        List<IPackage>? ContainingPackages { get; set; }

        /// <summary>
        /// Name of vessel
        /// </summary>
        [HebrewTranslation(typeof(IVessel), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// System in which vessel is at
        /// </summary>
        [HebrewTranslation(typeof(IVessel), nameof(System))]
        IStorageSystem? System { get; set; }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        [HebrewTranslation(typeof(IVessel), nameof(Type))]
        string? Type { get; set; }

        public new Dictionary<string, object?> ToDictionary()
            => new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Type)] = Type,
                [nameof(System)] = System?.Name,
                [nameof(ContainingPackages)] = ContainingPackages is null ? null : string.Join(", ", ContainingPackages.Select(x => x.Id)),
            };

        /// <summary>
        /// Transfrom package object to a VesselRequest object
        /// </summary>
        /// <returns></returns>
        public IVesselRequest Request() => new VesselRequest() { Name = Name, Type = Type, SystemId = System?.Id };

        public IVessel Copy();

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single vessel given vessel ID
        /// </summary>
        /// <param name="id">vessel ID</param>
        Task<Tuple<IVessel, ErrorResponse>> Get(string? id);

        /// <summary>
        /// All objects
        /// </summary>
        Task<Tuple<List<IVessel>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all vessels which contain the searched text
        /// </summary>
        Task<Tuple<List<IVessel>, ErrorResponse>> Containing(string? SearchText);

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<IVessel, ErrorResponse>> Create(IVesselRequest req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IVessel, ErrorResponse>> Update(string? id, IVesselRequest req);

        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedVessel);

        /// <summary>
        /// All packages that are located in this system
        /// </summary>
        public async Task<Tuple<List<IPackage>, ErrorResponse>> Packages() => await Packages(Id);
    }

    public abstract class BaseVessel: Resource, IVessel
    {
        private string? _Name;
        private string? _Type = string.Empty;

        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        public string? Type { get => _Type; set => _Type = value?.Trim(); }

        public List<IPackage>? ContainingPackages { get; set; }

        public IStorageSystem? System { get; set; }

        // ABSTRACT METHODS

        protected abstract IVesselsRequests GetRequests();

        public abstract Task<Tuple<List<IVessel>, ErrorResponse>> Containing(string? SearchText);

        public abstract Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedVessel);

        public abstract IVessel Copy();

        // API RELATED FUNCTIONS

        public async Task<Tuple<IVessel, ErrorResponse>> Get(string? id) =>
            await GetRequests().GetById(id);

        public async Task<Tuple<List<IVessel>, ErrorResponse>> All() =>
            await GetRequests().GetAll();

        public async Task<Tuple<IVessel, ErrorResponse>> Create(IVesselRequest req) =>
            await GetRequests().Create(req);

        public async Task<Tuple<IVessel, ErrorResponse>> Update(string? id, IVesselRequest req)
            => await GetRequests().Update(id, req);
    }
}
