using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomUnitsRequests : IUnitsRequests
    {
        public Tuple<List<Unit>, ErrorResponse> GetUnits()
        {
            return new RandomGenericRequests().Request(RandomData.RandomUnits);
        }

        public Tuple<Unit, ErrorResponse> CreateUnit(UnitRequest unit)
        {
            return new RandomGenericRequests().Request(unit.Create(Unit.GetNextId()));
        }

        public Tuple<Unit, ErrorResponse> GetUnit(string unit_id)
        {
            return new RandomGenericRequests().Request(RandomData.RandomUnit, canBeNotFound: true, canBadRequest: false);
        }

        public Tuple<Unit, ErrorResponse> UpdateUnit(string unit_id, UnitRequest unit)
        {
            return new RandomGenericRequests().Request(unit.Create(unit_id), canBeNotFound: true);
        }
    }
}
