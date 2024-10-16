﻿using CipherData.Randomizer;

namespace CipherData.Models
{
    [HebrewTranslation(nameof(Vessel))]
    public class Vessel : Resource
    {
        private string? _Name;
        private string? _Type = string.Empty;

        /// <summary>
        /// Name of vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string? Type { get => _Type; set => _Type = value?.Trim(); }       
            
        /// <summary>
        /// Packages within the vessel
        /// </summary>
        [HebrewTranslation(typeof(Vessel), nameof(ContainingPackages))]
        public List<Package>? ContainingPackages { get; set; }

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
        public VesselRequest Request() => new() { Name = Name, Type = Type, SystemId = System.Id };

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

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId() => $"V{++IdCounter:D3}";

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
                ContainingPackages = new List<Package>() { new("VP") }
            };
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single vessel given vessel ID
        /// </summary>
        /// <param name="id">vessel ID</param>
        /// <returns></returns>
        public static Tuple<Vessel, ErrorResponse> Get(string id) => Config.VesselsRequests.GetVessel(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Vessel>, ErrorResponse> All() => Config.VesselsRequests.GetVessels();

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
