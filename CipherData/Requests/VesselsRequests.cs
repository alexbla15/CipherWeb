using CipherData.Models;

namespace CipherData.Requests
{
    public class VesselsRequests
    {
        /// <summary>
        /// Get all vessels available.
        /// Path: Get /vessels/
        /// </summary>
        public static Tuple<List<Vessel>, ErrorResponse> GetVessels()
        {
            return GenericRequests.Request(RandomData.RandomVessels);
        }

        /// <summary>
        /// Create a new vessel.
        /// Path: POST /vessels
        /// </summary>
        public static Tuple<Vessel,ErrorResponse> CreateVessel(VesselRequest vessel)
        {
            return GenericRequests.Request(vessel.Create(Vessel.GetNextId()));
        }

        /// <summary>
        /// Get details about a single vessel.
        /// Path: Get /vessels/{id}
        /// </summary>
        public static Tuple<Vessel, ErrorResponse> GetVessel(string vessel_id)
        {
            return GenericRequests.Request(RandomData.RandomVessel, canBeNotFound:true, canBadRequest:false);
        }

        /// <summary>
        /// Update vessel's details
        /// Path: PUT /vessels/{id}
        /// </summary>
        public static Tuple<Vessel, ErrorResponse> UpdateVessel(string vessel_id, VesselRequest vessel)
        {
            return GenericRequests.Request(vessel.Create(vessel_id), canBeNotFound: true);
        }
    }
}
