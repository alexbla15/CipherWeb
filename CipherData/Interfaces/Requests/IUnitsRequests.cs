namespace CipherData.Interfaces
{
    public interface IUnitsRequests
    {
        /// <summary>
        /// Get all vessels available.
        /// Path: Get /units/
        /// </summary>
        
        Task<Tuple<List<IUnit>, ErrorResponse>> GetUnits();

        /// <summary>
        /// Create a new unit.
        /// Path: POST /units
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        Task<Tuple<IUnit, ErrorResponse>> CreateUnit(IUnitRequest unit);

        /// <summary>
        /// Get details about a single Unit.
        /// Path: Get /units/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        Task<Tuple<IUnit, ErrorResponse>> GetUnit(string unit_id);

        /// <summary>
        /// Update Unit's details
        /// Path: PUT /systems/{id}
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        Task<Tuple<IUnit, ErrorResponse>> UpdateUnit(string unit_id, IUnitRequest unit);
    }
}
