﻿using CipherData.Models;
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
        public static Tuple<Vessel?,ErrorResponse> CreateVessel(VesselRequest vessel)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1) 
            {
                return new Tuple<Vessel?, ErrorResponse>(new Vessel(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Vessel?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Vessel?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get details about a single vessel.
        /// Path: Get /vessels/{id}
        /// </summary>
        /// <param name="vessel_id"></param>
        /// <returns></returns>
        public static Tuple<Vessel?, ErrorResponse> GetVessel(string vessel_id)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<Vessel?, ErrorResponse>(new Vessel(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Vessel?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else
            {
                return new Tuple<Vessel?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Update vessel's details
        /// Path: PUT /vessels/{id}
        /// </summary>
        /// <returns></returns>
        public static Tuple<Vessel?, ErrorResponse> UpdateVessel(string vessel_id, VesselRequest vessel)
        {
            // an example for each of the 4 options
            Random rand = new();

            int result = rand.Next(1, 4);
            if (result == 1)
            {
                return new Tuple<Vessel?, ErrorResponse>(new Vessel(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Vessel?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else if (result == 3)
            {
                return new Tuple<Vessel?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Vessel?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }
    }
}
