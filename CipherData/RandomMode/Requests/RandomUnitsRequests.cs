namespace CipherData.RandomMode
{
    public class RandomUnitsRequests : IUnitsRequests
    {
        public async Task<Tuple<List<IUnit>, ErrorResponse>> GetUnits()
            => await new RandomGenericRequests().Request(RandomData.Units);

        public async Task<Tuple<IUnit, ErrorResponse>> CreateUnit(IUnitRequest unit)
            => await new RandomGenericRequests().Request(unit.Create(RandomUnit.GetNextId()));

        public async Task<Tuple<IUnit, ErrorResponse>> GetUnit(string unit_id)
            => await new RandomGenericRequests().Request(RandomData.Unit, canBeNotFound: true, canBadRequest: false);

        public async Task<Tuple<IUnit, ErrorResponse>> UpdateUnit(string unit_id, IUnitRequest unit)
            => await new RandomGenericRequests().Request(unit.Create(unit_id), canBeNotFound: true);
    }
}
