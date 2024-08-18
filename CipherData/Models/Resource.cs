using CipherData.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    public abstract class Resource
    {
        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        public string ClearenceLevel { get; set; } = GetRandomClearance();


        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        public int Uuid { get; set; } = GetUuid();

        private static int UuidCounter { get; set; } = 0;

        private static int GetUuid()
        {
            UuidCounter += 1;
            return UuidCounter;
        }

        public static readonly List<string> clearences = new() { "מוגבל", "מוגבל מאוד", "חופשי" };

        public static string GetRandomClearance()
        {
            Random random = new();
            return clearences[random.Next(0, clearences.Count - 1)];
        }

        /// <summary>
        /// Method to get all (english, hebrew) translations of the above attributes.
        /// </summary>
        /// <returns></returns>
        public static readonly HashSet<Tuple<string, string>> BasicHeaders = new()
        {
            new ("Id", "שם"),
            new ("Uuid", "מספר סידורי"),
            new ("ClearenceLevel", "מידור")
        };
    }
}
