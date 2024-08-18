namespace CipherData.Models
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    public class VesselRequest
    {
        /// <summary>
        /// Vessel type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Id of system containing vessel
        /// </summary>
        public string? SystemId { get; set; }

        /// <summary>
        /// Create a new unit or update it
        /// </summary>
        /// <param name="type">Vessel type</param>
        /// <param name="systemId">Id of system containing vessel</param>
        public VesselRequest(string type, string? systemId = null)
        {
            Type = type;
            SystemId = systemId;
        }
    }
}
