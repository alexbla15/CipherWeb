namespace CipherData.RandomMode
{
    public class RandomVesselsRequests : IVesselsRequests
    {
        public async Task<Tuple<List<IVessel>, ErrorResponse>> GetAll()
            => await new RandomGenericRequests().Request(RandomData.GetRandomVessels(new Random().Next(20)));

        public async Task<Tuple<IVessel, ErrorResponse>> Create(IVesselRequest vessel)
            => await new RandomGenericRequests().Request(vessel.Create(RandomVessel.GetNextId()));

        public async Task<Tuple<IVessel, ErrorResponse>> GetById(string? vessel_id)
            => await new RandomGenericRequests().Request(new RandomVessel() { Id=vessel_id} as IVessel, canBeNotFound: true, canBadRequest: false);

        public async Task<Tuple<IVessel, ErrorResponse>> Update(string? vessel_id, IVesselRequest vessel)
            => await new RandomGenericRequests().Request(vessel.Create(vessel_id), canBeNotFound: true);
    }
}
