using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Vessel
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of vessel (bottle / pot / ...)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Safety restrictions in a list of (MaterialType, SubCategory, Amount)
        /// </summary>
        public List<Tuple<string, string, decimal>> Restrictions { get; set; }

        /// <summary>
        /// Name of storage-system in which its located
        /// </summary>
        public string Storage { get; set; }
        
        /// <summary>
        /// Date when it was opened
        /// </summary>
        public DateTime OpenDate { get; set; }


        /// <summary>
        /// Date when it will be expired (can't store any packages)
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }
}
