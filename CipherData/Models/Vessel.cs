using CipherData.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CipherData.Models
{
    public class Vessel : Resource
    {
        /// <summary>
        /// Name of vessel
        /// </summary>
        [HebrewTranslation("שם")]
        public string? Name { get; set; }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        [HebrewTranslation("סוג")]
        public string Type { get; set; }

        /// <summary>
        /// Packages within the vessel
        /// </summary>
        [HebrewTranslation("תעודות מוכלות")]
        public HashSet<Package>? ContainingPackages { get; set; }

        /// <summary>
        /// System in which vessel is at
        /// </summary>
        [HebrewTranslation("מערכת")]
        public StorageSystem System { get; set; }

        /// <summary>
        /// Vessel containing some packages, inside some system
        /// </summary>
        /// <param name="name">name of vessel</param>
        /// <param name="type">User ID of user who made the action. Required.</param>
        /// <param name="system">Full-text user comment on action.</param>
        /// <param name="packages"> Safety restrictions in a list of (MaterialType, SubCategory, Amount)</param>
        public Vessel(string type, StorageSystem system, HashSet<Package>? packages = null, string? name = null, string? id = null)
        {
            string nextId = GetNextId();

            Id = id ?? nextId;
            Name = name ?? nextId;
            Type = type;
            ContainingPackages = packages;
            System = system;
        }

        /// <summary>
        /// Method to get an empty Vessel object scheme
        /// </summary>
        public static Vessel Empty()
        {
            return new Vessel(
                id: string.Empty,
                name: string.Empty,
                type: string.Empty,
                system: StorageSystem.Empty()
                );
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId()
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

            return new Vessel(
                id: id,
                name: id,
                type: RandomFuncs.RandomItem(VesselTypes),
                system: StorageSystem.Random()
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(Vessel), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

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
            return GetObjects<Vessel>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: $"{typeof(Vessel).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Vessel).Name}.{nameof(Name)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Vessel).Name}.{nameof(Type)}", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Vessel).Name}.{nameof(System)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: $"{typeof(Vessel).Name}.{nameof(ContainingPackages)}.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
                    }, @operator: Operator.Or));
        }
    }
}
