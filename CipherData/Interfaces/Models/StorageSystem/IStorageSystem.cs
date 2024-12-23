﻿using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(IStorageSystem))]
    public interface IStorageSystem : IResource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(Description))]
        string ? Description { get; set; }

        /// <summary>
        /// Name of the system
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(Children))]
        List<IStorageSystem>? Children { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(Parent))]
        IStorageSystem? Parent { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(Properties))]
        Dictionary<string, string>? Properties { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>
        [HebrewTranslation(typeof(IStorageSystem), nameof(Unit))]
        IUnit? Unit { get; set; }

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

        public ISystemRequest Request() =>
            new SystemRequest()
            {
                ParentId = Parent?.Id,
                Name = Name,
                Description = Description,
                Properties = Properties,
                UnitId=Unit?.Id
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        // API RELATED FUNCTIONS

        /// <summary>
        /// Get details about a system vessel given system ID
        /// </summary>
        /// <param name="id">system ID</param>
        Task<Tuple<IStorageSystem, ErrorResponse>> Get(string? id);

        /// <summary>
        /// All systems that took place in a certain system
        /// </summary>
        Task<Tuple<List<IStorageSystem>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all systems which contain the searched text
        /// </summary>
        Task<Tuple<List<IStorageSystem>, ErrorResponse>> Containing(string? SearchText);

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<IStorageSystem, ErrorResponse>> Create(ISystemRequest req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IStorageSystem, ErrorResponse>> Update(string? id, ISystemRequest req);

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public async Task<Tuple<List<IEvent>, ErrorResponse>> Events() => await Events(Id);

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public async Task<Tuple<List<IProcess>, ErrorResponse>> Processes() => await Processes(Id);

        /// <summary>
        /// All packages that are located in this system
        /// </summary>
        public async Task<Tuple<List<IPackage>, ErrorResponse>> Packages() => await Packages(Id);

        /// <summary>
        /// All vessels that are located in this system
        /// </summary>
        public async Task<Tuple<List<IVessel>, ErrorResponse>> Vessels() => await Vessels(Id);

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        Task<Tuple<List<IEvent>, ErrorResponse>> Events(string? SelectedSystem);

        /// <summary>
        /// All processes that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        /// <returns></returns>
        Task<Tuple<List<IProcess>, ErrorResponse>> Processes(string? SelectedSystem);

        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedSystem);

        /// <summary>
        /// All vessels that are under this system
        /// </summary>
        Task<Tuple<List<IVessel>, ErrorResponse>> Vessels(string? SelectedSystem);
    }

    public abstract class BaseStorageSystem: Resource, IStorageSystem
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public Dictionary<string, string>? Properties { get; set; }

        public IStorageSystem? Parent { get; set; }

        public List<IStorageSystem>? Children { get; set; }

        public IUnit? Unit { get; set; }

        // ABSTRACT METHODS

        protected abstract ISystemsRequests GetRequests();

        public abstract Task<Tuple<List<IStorageSystem>, ErrorResponse>> Containing(string? SearchText);

        public abstract Task<Tuple<List<IEvent>, ErrorResponse>> Events(string? SelectedSystem);

        public abstract Task<Tuple<List<IProcess>, ErrorResponse>> Processes(string? SelectedSystem);

        public abstract Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedSystem);

        public abstract Task<Tuple<List<IVessel>, ErrorResponse>> Vessels(string? SelectedSystem);

        // API RELATED FUNCTIONS

        public async Task<Tuple<IStorageSystem, ErrorResponse>> Get(string? id) =>
            await GetRequests().GetById(id);

        public async Task<Tuple<List<IStorageSystem>, ErrorResponse>> All() =>
            await GetRequests().GetAll();

        public async Task<Tuple<IStorageSystem, ErrorResponse>> Create(ISystemRequest req) =>
            await GetRequests().Create(req);

        public async Task<Tuple<IStorageSystem, ErrorResponse>> Update(string? id, ISystemRequest req)
            => await GetRequests().Update(id, req);
    }
}
