using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class SubCategory
    {
        /// <summary>
        /// unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of a the main-category containing this sub-category
        /// </summary>
        public string MainCategory { get; set; }

        /// <summary>
        /// Full text description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Mask of package-serial-number of items from this sub-category
        /// </summary>
        public string Mask { get; set; }

        /// <summary>
        /// Type of material of this category
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// defined properties of packages from this sub-category
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }
        
        /// <summary>
        /// Names of processes that can use items from this category as input
        /// </summary>
        public List<string> InProcesses { get; set; }


        /// <summary>
        /// Names of processes that can create items from this category as output
        /// </summary>
        public List<string> OutProcesses { get; set; }

        /// <summary>
        /// Date when the attributes of this sub-category were last updated
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
