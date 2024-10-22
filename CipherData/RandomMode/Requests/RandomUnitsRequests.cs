namespace CipherData.RandomMode
{
    public class RandomUnitsRequests : IUnitsRequests
    {
        public async Task<Tuple<List<IUnit>, ErrorResponse>> GetAll()
            => await new RandomGenericRequests().Request(RandomData.Units);

        public async Task<Tuple<IUnit, ErrorResponse>> Create(IUnitRequest unit)
            => await new RandomGenericRequests().Request(unit.Create<RandomUnit>(RandomUnit.GetNextId()));

        public async Task<Tuple<IUnit, ErrorResponse>> GetById(string? unit_id)
            => await new RandomGenericRequests().Request(RandomData.Unit, canBeNotFound: true, canBadRequest: false);

        public async Task<Tuple<IUnit, ErrorResponse>> Update(string? unit_id, IUnitRequest unit)
            => await new RandomGenericRequests().Request(unit.Create<RandomUnit>(unit_id), canBeNotFound: true);
    }
}
