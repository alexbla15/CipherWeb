﻿using CipherData.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Category: Resource
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of a the main-category containing this sub-category
        /// </summary>
        //public string MainCategory { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        public HashSet<string> IdMask { get; set; }

        /// <summary>
        /// Type of material of this category
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        public HashSet<ProcessDefinition> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        public HashSet<ProcessDefinition> ConsumingProcesses { get; set; }

        private static int IdCounter { get; set; } = 0;

        public static string GetId()
        {
            IdCounter += 1;
            return $"C{IdCounter}";
        }

        /// <summary>
        /// Instanciation of new Category.
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="idMask">List of ID masks to identify the category from the package ID</param>
        /// <param name="materialType">Type of material of this category</param>
        /// <param name="creatingProcesses">List of processes definitions creating this category</param>
        /// <param name="consumingProcesses">List of processes defintions consuming this category</param>
        public Category(string name, string description, HashSet<string> idMask, string materialType, 
            HashSet<ProcessDefinition> creatingProcesses, HashSet<ProcessDefinition> consumingProcesses, string? id = null)
        {
            Id = id ?? GetId();
            Name = name;
            Description = description;
            IdMask = idMask;
            MaterialType = materialType;
            CreatingProcesses = creatingProcesses;
            ConsumingProcesses = consumingProcesses;
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static HashSet<Tuple<string, string>> Headers()
        {
            HashSet<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Name", "שם"));
            result.Add(new("Description", "תיאור"));
            result.Add(new("IdMask", "סדרה"));
            result.Add(new("MaterialType", "סוג החומר"));
            result.Add(new("CreatingProcesses", "תהליכי יצירה"));
            result.Add(new("ConsumingProcesses", "תהליכי צריכה"));

            return result;
        }

        public static Category Random(string? id = null)
        {
            return new Category(
                id: id,
                name: Globals.GetRandomString(Globals.CategoriesNames),
                description: Globals.GetRandomString(Globals.CategoriesDescriptions),
                idMask: new HashSet<string>() { Globals.GetRandomString(Globals.IdMasks) },
                creatingProcesses: new List<ProcessDefinition>().ToHashSet(),
                consumingProcesses: new List<ProcessDefinition>().ToHashSet(),
                materialType: Globals.GetRandomString(Globals.MaterialTypes)

                );
        }
    }
}
