using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class VesselsRequests
    {
        /// <summary>
        /// Create a new vessel.
        /// Path: POST /vessels
        /// </summary>
        /// <param name="vessel"></param>
        /// <returns></returns>
        public static Tuple<Vessel,ErrorResponse> CreateVessel(VesselRequest vessel)
        {
            return GenericRequests.Request(Vessel.Random());
        }

        /// <summary>
        /// Get details about a single vessel.
        /// Path: Get /vessels/{id}
        /// </summary>
        /// <param name="vessel_id"></param>
        /// <returns></returns>
        public static Tuple<Vessel, ErrorResponse> GetVessel(string vessel_id)
        {
            return GenericRequests.Request(Vessel.Random(vessel_id), canBeNotFound:true, canBadRequest:false);
        }

        /// <summary>
        /// Update vessel's details
        /// Path: PUT /vessels/{id}
        /// </summary>
        /// <returns></returns>
        public static Tuple<Vessel, ErrorResponse> UpdateVessel(string vessel_id, VesselRequest vessel)
        {
            return GenericRequests.Request(Vessel.Random(vessel_id), canBeNotFound: true);
        }
    }
}
