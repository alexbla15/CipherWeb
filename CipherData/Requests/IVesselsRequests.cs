using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IVesselsRequests
    {
        /// <summary>
        /// Get all vessels available.
        /// Path: Get /vessels/
        /// </summary>
        Tuple<List<Vessel>, ErrorResponse> GetVessels();

        /// <summary>
        /// Create a new vessel.
        /// Path: POST /vessels
        /// </summary>
        Tuple<Vessel, ErrorResponse> CreateVessel(VesselRequest vessel);

        /// <summary>
        /// Get details about a single vessel.
        /// Path: Get /vessels/{id}
        /// </summary>
        Tuple<Vessel, ErrorResponse> GetVessel(string vessel_id);

        /// <summary>
        /// Update vessel's details
        /// Path: PUT /vessels/{id}
        /// </summary>
        Tuple<Vessel, ErrorResponse> UpdateVessel(string vessel_id, VesselRequest vessel);
    }
}
