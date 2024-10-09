using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IVesselsRequests
    {
        /// <summary>
        /// Get all vessels available.
        /// Path: Get /vessels/
        /// </summary>
        Tuple<List<IVessel>, ErrorResponse> GetVessels();

        /// <summary>
        /// Create a new vessel.
        /// Path: POST /vessels
        /// </summary>
        Tuple<IVessel, ErrorResponse> CreateVessel(VesselRequest vessel);

        /// <summary>
        /// Get details about a single vessel.
        /// Path: Get /vessels/{id}
        /// </summary>
        Tuple<IVessel, ErrorResponse> GetVessel(string vessel_id);

        /// <summary>
        /// Update vessel's details
        /// Path: PUT /vessels/{id}
        /// </summary>
        Tuple<IVessel, ErrorResponse> UpdateVessel(string vessel_id, VesselRequest vessel);
    }
}
