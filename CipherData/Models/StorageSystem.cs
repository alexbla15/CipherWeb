﻿using CipherData.Requests;

namespace CipherData.Models
{
    public class StorageSystem: Resource
    {
        /// <summary>
        /// Description of system
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        public StorageSystem? Parent { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        public HashSet<StorageSystem>? Children { get; set; }

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>
        public Unit Unit { get; set; }

        /// <summary>
        /// Instanciation of StorageSystem
        /// </summary>
        /// <param name="description">Description of system</param>
        /// <param name="properties">JSON-like additional properties of the system</param>
        /// <param name="unit">Unit responsible for this system.</param>
        /// <param name="parent">Parent system containing this one</param>
        /// <param name="children">Child systems contained in this one</param>
        public StorageSystem(string description, string properties, Unit unit,
            StorageSystem? parent = null, HashSet<StorageSystem>? children = null, string? id = null)
        {
            Id = id ?? GetNextId();
            Description = description;
            Properties = properties;
            Parent = parent;
            Children = children;
            Unit = unit;
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
            return $"S{IdCounter:D3}";
        }
        
        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Description", "תיאור"));
            result.Add(new("Properties", "תכונות"));
            result.Add(new("Parent", "מערכת אב"));
            result.Add(new("Children", "מערכות מוכלות"));
            result.Add(new("Unit", "יחידה"));

            return result;
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static StorageSystem Random(string? id = null)
        {
            return new(
                id: id,
                description: Globals.GetRandomString(Globals.SystemsDescriptions),
                properties: "",
                unit: Unit.Random()
                );
        }

        /// <summary>
        /// Fetch all systems which contain the searched text
        /// </summary>
        public static Tuple<List<StorageSystem>?, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<StorageSystem>(SearchText, searchText => new GroupedBooleanCondition(conditions: new() {
        new BooleanCondition(attribute: "System.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "System.Description", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "System.Properties", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "System.Parent.Id", attributeRelation: AttributeRelation.Contains, value: SearchText),
                new BooleanCondition(attribute: "System.Children.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or),
                new BooleanCondition(attribute: "System.Unit.Id", attributeRelation: AttributeRelation.Contains, value: SearchText, @operator:Operator.Or)
                                }, @operator: Operator.Or));
        }

    }
}
