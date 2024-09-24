using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IUnitsRequests
    {
        /// <summary>
        /// Get all vessels available.
        /// Path: Get /units/
        /// </summary>
        Tuple<List<Unit>, ErrorResponse> GetUnits();

        /// <summary>
        /// Create a new unit.
        /// Path: POST /units
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        Tuple<Unit, ErrorResponse> CreateUnit(UnitRequest unit);

        /// <summary>
        /// Get details about a single Unit.
        /// Path: Get /units/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        Tuple<Unit, ErrorResponse> GetUnit(string unit_id);

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /systems/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        Tuple<Unit, ErrorResponse> UpdateUnit(string unit_id, UnitRequest unit);
    }
}
