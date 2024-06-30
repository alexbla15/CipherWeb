using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class Package
    {
        /// <summary>
        /// Unique identifier (running number)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique serial number of this package
        /// </summary>
        public string SerialNumber { get; set; }

        public string? Material { get; set; }

        /// <summary>
        /// Sub category name of this package
        /// </summary>
        public string SubCategory { get; set; }

        /// <summary>
        /// Vessel name which contains the package
        /// </summary>
        public string Vessel { get; set; }
        
        /// <summary>
        /// Brut-mass of this package
        /// </summary>
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net-mass of this package
        /// </summary>
        public decimal NetMass { get; set; }
        
        /// <summary>
        /// Date when package was opened
        /// </summary>
        public DateTime OpenDate { get; set; }  

        /// <summary>
        /// Full-text comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Specific properties of package (e.g. color...)
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }
    }
}
