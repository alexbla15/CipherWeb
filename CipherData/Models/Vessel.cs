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
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Packages within the vessel
        /// </summary>
        public HashSet<Package>? ContainingPackages { get; set; }

        /// <summary>
        /// System in which vessel is at
        /// </summary>
        public StorageSystem System { get; set; }

        /// <summary>
        /// Vessel containing some packages, inside some system
        /// </summary>
        /// <param name="type">User ID of user who made the action. Required.</param>
        /// <param name="system">Full-text user comment on action.</param>
        /// <param name="packages"> Safety restrictions in a list of (MaterialType, SubCategory, Amount)</param>
        public Vessel(string type, StorageSystem system, HashSet<Package>? packages = null, string? id = null)
        {
            Id = id ?? GetNextId();
            Type = type;
            ContainingPackages = packages;
            System = system;
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
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Type", "סוג"));
            result.Add(new("ContainingPackages", "תעודות מוכלות"));
            result.Add(new("System", "מערכת"));

            return result;
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Vessel Random(string? id = null)
        {
            return new Vessel(
                id: id,
                type: Globals.GetRandomString(Globals.VesselTypes),
                system: StorageSystem.Random()
                );
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Fetch all vessels which contain the searched text
        /// </summary>
        public static Tuple<List<Vessel>?, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Vessel>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
                new BooleanCondition(attribute: "Vessel.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Vessel.Type", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Vessel.System.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "Vessel.ContainingPackages.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
                    }, @operator: Operator.Or));
        }
    }
}
