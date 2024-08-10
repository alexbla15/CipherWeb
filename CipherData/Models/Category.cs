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
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Name of a the main-category containing this sub-category
        /// </summary>
        //public string MainCategory { get; set; }

        /// <summary>
        /// Free-text description of the category
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// List of ID masks to identify the category from the package ID
        /// </summary>
        public List<string> IdMask { get; set; }

        /// <summary>
        /// Type of material of this category
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// defined properties of packages from this sub-category
        /// </summary>
        //public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// List of processes definitions creating this category
        /// </summary>
        public List<ProcessDefinition> CreatingProcesses { get; set; }


        /// <summary>
        /// List of processes defintions consuming this category
        /// </summary>
        public List<ProcessDefinition> ConsumingProcesses { get; set; }

        /// <summary>
        /// Date when the attributes of this sub-category were last updated
        /// </summary>
        //public DateTime UpdateDate { get; set; }


        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public static List<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = BasicHeaders;

            result.Add(new("Name", "שם"));
            result.Add(new("Description", "תיאור"));
            result.Add(new("IdMask", "סדרה"));
            result.Add(new("MaterialType", "סוג החומר"));
            result.Add(new("CreatingProcesses", "תהליכי יצירה"));
            result.Add(new("ConsumingProcesses", "תהליכי צריכה"));

            return result;
        }
    }
}
