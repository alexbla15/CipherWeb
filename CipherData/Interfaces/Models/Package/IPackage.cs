﻿using System.Reflection;

namespace CipherData.Interfaces
{
    public interface IPackage : IResource
    {
        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(BrutMass))]
        decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(NetMass))]
        decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(CreatedAt))]
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Dictionary of additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Properties))]
        List<IPackageProperty>? Properties { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Category))]
        ICategory Category { get; set; }

        /// <summary>
        /// Calculated from the ratio between net to brut mass
        /// </summary>
        decimal Concentration { get; }

        /// <summary>
        /// List of processes definitions that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(DestinationProcesses))]
        List<IProcessDefinition> DestinationProcesses { get; set; }

        /// <summary>
        /// Parent package containing this one.
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Parent))]
        IPackage? Parent { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Children))]
        List<IPackage>? Children { get; set; }

        /// <summary>
        /// Location which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(System))]
        IStorageSystem System { get; set; }

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Vessel))]
        IVessel? Vessel { get; set; }

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(BrutMass)] = BrutMass,
                [nameof(NetMass)] = NetMass,
                [nameof(Category)] = Category.Name,
                [nameof(Description)] = Description,
                [nameof(System)] = System.Id,
                [nameof(Vessel)] = Vessel?.Id,
                [nameof(Parent)] = Parent?.Id,
                [nameof(Children)] = Children != null ? string.Join("; ", Children.Select(x => x.Id)) : null,
                [nameof(DestinationProcesses)] = string.Join("; ", DestinationProcesses.Select(x => x.Name)),
                [nameof(Properties)] = Properties != null ? string.Join("; ", Properties.Select(x => $"{x.Name}:{x.Value}")) : null,
                [nameof(CreatedAt)] = CreatedAt,
            };
        }

        /// <summary>
        /// Transfrom package object to a PackageRequest object
        /// </summary>
        public IPackageRequest Request() =>
            new PackageRequest()
            {
                Id = Id,
                BrutMass = BrutMass,
                NetMass = NetMass,
                Properties = Properties,
                ParentId = Parent?.Id,
                ChildrenIds = Children?.Select(x => x.Id).ToList(),
                SystemId = System.Id,
                VesselId = Vessel?.Id,
                CategoryId = Category.Id
            };


        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All packages
        /// </summary>
        Task<Tuple<List<IPackage>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        Task<Tuple<List<IPackage>, ErrorResponse>> Containing(string SearchText);

        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="id">package ID</param>
        Task<Tuple<IPackage, ErrorResponse>> Get(string? id);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IPackage, ErrorResponse>> Update(string? id, IUpdatePackage req);

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        Task<Tuple<List<IEvent>, ErrorResponse>> Events();

        /// <summary>
        /// All processes relevant for package.
        /// </summary>
        Task<Tuple<List<IProcess>, ErrorResponse>> Processes();
    }
}
