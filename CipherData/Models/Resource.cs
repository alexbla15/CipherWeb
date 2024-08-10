using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public abstract class Resource
    {
        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        public string ClearenceLevel { get; set; } = string.Empty;


        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        public int Uuid { get; set; }

        /// <summary>
        /// Method to get all (english, hebrew) translations of the above attributes.
        /// </summary>
        /// <returns></returns>
        public static List<Tuple<string, string>> BasicHeaders = new List<Tuple<string, string>>()
        {
            new Tuple<string,string> ("Id", "שם"),
            new Tuple<string,string> ("Uuid", "מספר סידורי"),
            new Tuple<string,string> ("ClearenceLevel", "מידור")
        };
    }
}
