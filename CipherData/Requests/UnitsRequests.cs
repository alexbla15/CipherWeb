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
        /// Get all vessels available.
        /// Path: Get /units/
        /// </summary>
        public static Tuple<List<Unit>, ErrorResponse> GetUnits()
        {
            return GenericRequests.Request(RandomData.RandomUnits);
        }

        /// <summary>
        /// Create a new unit.
        /// Path: POST /units
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Tuple<Unit,ErrorResponse> CreateUnit(UnitRequest unit)
        {
            return GenericRequests.Request(RandomData.RandomUnit);
        }

        /// <summary>
        /// Get details about a single Unit.
        /// Path: Get /units/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public static Tuple<Unit, ErrorResponse> GetUnit(string unit_id)
        {
            return GenericRequests.Request(RandomData.RandomUnit, canBeNotFound:true, canBadRequest:false);
        }

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /systems/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public static Tuple<Unit, ErrorResponse> UpdateUnit(string unit_id, UnitRequest unit)
        {
            return GenericRequests.Request(RandomData.RandomUnit, canBeNotFound: true);
        }
    }
}
