using CipherData.Models;
using CipherData.Models.Randomizers;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomUnitsRequests : IUnitsRequests
    {
        public Tuple<List<IUnit>, ErrorResponse> GetUnits()
            => new RandomGenericRequests().Request(RandomData.Units);

        public Tuple<IUnit, ErrorResponse> CreateUnit(IUnitRequest unit)
            => new RandomGenericRequests().Request(unit.Create(RandomUnit.GetNextId()));

        public Tuple<IUnit, ErrorResponse> GetUnit(string unit_id)
            => new RandomGenericRequests().Request(RandomData.Unit, canBeNotFound: true, canBadRequest: false);

        public Tuple<IUnit, ErrorResponse> UpdateUnit(string unit_id, IUnitRequest unit)
            => new RandomGenericRequests().Request(unit.Create(unit_id), canBeNotFound: true);
    }
}
