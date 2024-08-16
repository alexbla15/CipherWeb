using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class UnitsRequests
    {
        /// <summary>
        /// Create a new unit.
        /// Path: POST /units
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Tuple<Unit?,ErrorResponse> CreateUnit(UnitRequest unit)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1) 
            {
                return new Tuple<Unit?, ErrorResponse>(new Unit(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get details about a single Unit.
        /// Path: Get /units/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public static Tuple<Unit?, ErrorResponse> GetUnit(string unit_id)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<Unit?, ErrorResponse>(new Unit(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /systems/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public static Tuple<Unit?, ErrorResponse> UpdateUnit(string unit_id, UnitRequest unit)
        {
            // an example for each of the 4 options
            Random rand = new();

            int result = rand.Next(1, 4);
            if (result == 1)
            {
                return new Tuple<Unit?, ErrorResponse>(new Unit(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else if (result == 3)
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Unit?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }
    }
}
