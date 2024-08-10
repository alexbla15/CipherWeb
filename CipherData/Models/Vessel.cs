using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Vessel: Resource
    {
        /// <summary>
        /// Unique name
        /// </summary>
        //public string Name { get; set; }

        /// <summary>
        /// Vessel type (bottle / pot / ...)
        /// </summary>
        public string Type { get; set; }

        public List<Package> ContainingPackages { get; set; }

        /// <summary>
        /// System in which vessel is at
        /// </summary>
        public StorageSystem System { get; set; }

        /// <summary>
        /// Safety restrictions in a list of (MaterialType, SubCategory, Amount)
        /// </summary>
        public List<Tuple<string, string, decimal>> Restrictions { get; set; }
                
        /// <summary>
        /// Date when it was opened
        /// </summary>
        //public DateTime OpenDate { get; set; }


        /// <summary>
        /// Date when it will be expired (can't store any packages)
        /// </summary>
        //public DateTime ExpirationDate { get; set; }
    }
}
