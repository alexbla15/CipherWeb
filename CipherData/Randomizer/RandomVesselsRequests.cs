using CipherData.Models;
using CipherData.Models.Randomizers;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{

    public class RandomVesselsRequests : IVesselsRequests
    {
        public Tuple<List<IVessel>, ErrorResponse> GetVessels()
            => new RandomGenericRequests().Request(RandomData.Vessels);

        public Tuple<IVessel, ErrorResponse> CreateVessel(IVesselRequest vessel)
            => new RandomGenericRequests().Request(vessel.Create(RandomVessel.GetNextId()));

        public Tuple<IVessel, ErrorResponse> GetVessel(string vessel_id)
            => new RandomGenericRequests().Request(RandomData.Vessel, canBeNotFound: true, canBadRequest: false);

        public Tuple<IVessel, ErrorResponse> UpdateVessel(string vessel_id, IVesselRequest vessel)
            => new RandomGenericRequests().Request(vessel.Create(vessel_id), canBeNotFound: true);
    }
}
