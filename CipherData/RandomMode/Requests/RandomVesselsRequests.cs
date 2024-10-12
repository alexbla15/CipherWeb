namespace CipherData.RandomMode
{

    public class RandomVesselsRequests : IVesselsRequests
    {
        public async Task<Tuple<List<IVessel>, ErrorResponse>> GetVessels()
            => await new RandomGenericRequests().Request(RandomData.Vessels);

        public async Task<Tuple<IVessel, ErrorResponse>> CreateVessel(IVesselRequest vessel)
            => await new RandomGenericRequests().Request(vessel.Create(RandomVessel.GetNextId()));

        public async Task<Tuple<IVessel, ErrorResponse>> GetVessel(string vessel_id)
            => await new RandomGenericRequests().Request(RandomData.Vessel, canBeNotFound: true, canBadRequest: false);

        public async Task<Tuple<IVessel, ErrorResponse>> UpdateVessel(string vessel_id, IVesselRequest vessel)
            => await new RandomGenericRequests().Request(vessel.Create(vessel_id), canBeNotFound: true);
    }
}
