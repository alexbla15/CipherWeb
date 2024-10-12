namespace CipherData.Interfaces
{
    public interface IVesselsRequests
    {
        /// <summary>
        /// Get all vessels available.
        /// Path: Get /vessels/
        /// </summary>
        Task<Tuple<List<IVessel>, ErrorResponse>> GetVessels();

        /// <summary>
        /// Create a new vessel.
        /// Path: POST /vessels
        /// </summary>
        Task<Tuple<IVessel, ErrorResponse>> CreateVessel(IVesselRequest vessel);

        /// <summary>
        /// Get details about a single vessel.
        /// Path: Get /vessels/{id}
        /// </summary>
        Task<Tuple<IVessel, ErrorResponse>> GetVessel(string vessel_id);

        /// <summary>
        /// Update vessel's details
        /// Path: PUT /vessels/{id}
        /// </summary>
        Task<Tuple<IVessel, ErrorResponse>> UpdateVessel(string vessel_id, IVesselRequest vessel);
    }
}
