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
            return GenericRequests.Request(Unit.Random());
        }

        /// <summary>
        /// Get details about a single Unit.
        /// Path: Get /units/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public static Tuple<Unit?, ErrorResponse> GetUnit(string unit_id)
        {
            return GenericRequests.Request(Unit.Random(unit_id), canBeNotFound:true, canBadRequest:false);
        }

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /systems/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public static Tuple<Unit?, ErrorResponse> UpdateUnit(string unit_id, UnitRequest unit)
        {
            return GenericRequests.Request(Unit.Random(unit_id), canBeNotFound: true);
        }
    }
}
