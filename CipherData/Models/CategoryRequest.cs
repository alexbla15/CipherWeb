using System;
using static CipherData.Models.CreateEvent;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherData.Models
{
    /// <summary>
    /// Create a new category or update it
    /// </summary>
    public class CategoryRequest
    {

        /// <summary>
        /// Name of the category
        /// </summary>
        [HebrewTranslation(Translator.Category_Name)]
        public string Name { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        [HebrewTranslation(Translator.Category_Description)]
        public string Description { get; set; }

        /// <summary>
        /// Parent-category (ID) containing this one. Not necessarily Material-type.
        /// </summary>
        [HebrewTranslation(Translator.Category_Parent)]
        public string? ParentId { get; set; }

        /// <summary>
        /// List of processes definition IDs creating this category
        /// </summary>
        [HebrewTranslation(Translator.Category_CreatingProcesses)]
        public List<string> CreatingProcesses { get; set; }

        /// <summary>
        /// List of processes definition IDs consuming this category
        /// </summary>
        [HebrewTranslation(Translator.Category_ConsumingProcesses)]
        public List<string> ConsumingProcesses { get; set; }

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        [HebrewTranslation(Translator.Category_IdMask)]
        public List<string> IdMask { get; set; }

        /// <summary>
        /// Properties that are accurate to most of the packages of this category.
        /// </summary>
        [HebrewTranslation(Translator.Category_Properties)]
        public List<CategoryProperty>? Properties { get; set; }

        /// <summary>
        /// Create a new category or update it
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="parent">Parent-category (ID) containing this one. Not necessarily Material-type.</param>
        /// <param name="description">Free-text description of the category</param>
        /// <param name="creatingProcesses">List of processes definition IDs creating this category</param>
        /// <param name="consumingProcesses">List of processes definition IDs consuming this category</param>
        /// <param name="idMask">List of ID masks to identify the category from the package ID</param>
        public CategoryRequest(string name, string description, List<string> idMask,
            string? parent = null,
            List<string>? creatingProcesses = null, 
            List<string>? consumingProcesses = null, List<CategoryProperty>? properties = null)
        {
            Name = name;
            Description = description;
            ParentId = parent;
            CreatingProcesses = creatingProcesses ?? new List<string>();
            ConsumingProcesses = consumingProcesses ?? new List<string>();
            IdMask = idMask;
            Properties = properties;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new(true, string.Empty);

            result = (!string.IsNullOrEmpty(Name)) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.Name))); // required
            result = (!string.IsNullOrEmpty(Description)) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.Description))); // required
            result = (IdMask.Count > 0) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.IdMask))); // required
            result = (CreatingProcesses.Count > 0) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.CreatingProcesses))); // event type is required
            result = (ConsumingProcesses.Count > 0) ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.ConsumingProcesses))); // event type is required

            // check properties
            if (Properties != null && result.Item1)
            {
                // check uniqueness of the properties names
                List<string> props = Properties.Select(prop => prop.Name).ToList();
                result = props.Count == props.Distinct().Count() ? result : Tuple.Create(false, Category.Translate(nameof(RandomData.RandomCategory.Properties)));

                if (result.Item1)
                {
                    // check inner logic of each property
                    foreach (CategoryProperty prop in Properties)
                    {
                        result = prop.Check();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Checks for difference between this and another category
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(Category? OtherObject)
        {
            bool different = false;

            different |= Name != OtherObject?.Name;
            different |= Description != OtherObject?.Description;
            different |= ParentId != OtherObject?.Parent?.Id;
            different |= IdMask[0] != OtherObject?.IdMask[0];

            if ((CreatingProcesses != null) && (OtherObject?.CreatingProcesses != null))
            {
                different |= !CreatingProcesses.ToHashSet().SetEquals(OtherObject.CreatingProcesses.Select(x => x.Id).ToList());

            }
            else if (((CreatingProcesses != null) && (OtherObject?.CreatingProcesses == null)) || ((CreatingProcesses == null) && (OtherObject?.CreatingProcesses != null)))
            {
                different = true;
            }

            if ((ConsumingProcesses != null) && (OtherObject?.ConsumingProcesses != null))
            {
                different |= !ConsumingProcesses.ToHashSet().SetEquals(OtherObject.ConsumingProcesses.Select(x => x.Id).ToList());

            }
            else if (((ConsumingProcesses != null) && (OtherObject?.ConsumingProcesses == null)) || ((ConsumingProcesses == null) && (OtherObject?.ConsumingProcesses != null)))
            {
                different = true;
            }

            if ((Properties != null) && (OtherObject?.Properties != null))
            {
                // check for same property names
                different |= !Properties.Select(x => x.Name).ToHashSet().SetEquals(OtherObject.Properties.Select(x => x.Name).ToList());
                // check for differences
                if (!different)
                {
                    foreach (CategoryProperty prop in Properties)
                    {
                        CategoryProperty other_prop = OtherObject.Properties.Where(x => x.Name == prop.Name).ToList()[0];
                        different |= prop.Description != other_prop.Description;
                        different |= prop.PropertyType != other_prop.PropertyType;
                        different |= prop.DefaultValue != other_prop.DefaultValue;
                    }
                }
            }
            else if (((Properties != null) && (OtherObject?.Properties == null)) || ((Properties == null) && (OtherObject?.Properties != null)))
            {
                different = true;
            }

            return different;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return Resource.ToJson(this);
        }

        /// <summary>
        /// Return an empty object scheme.
        /// </summary>
        public static CategoryRequest Empty()
        {
            return new CategoryRequest(name: string.Empty, description: string.Empty, idMask: new List<string>());
        }
    }
}
