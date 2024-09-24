using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{

    public class RandomVesselsRequests : IVesselsRequests
    {
        public Tuple<List<Vessel>, ErrorResponse> GetVessels()
        {
            return new RandomGenericRequests().Request(RandomData.RandomVessels);
        }

        public Tuple<Vessel, ErrorResponse> CreateVessel(VesselRequest vessel)
        {
            return new RandomGenericRequests().Request(vessel.Create(Vessel.GetNextId()));
        }

        public Tuple<Vessel, ErrorResponse> GetVessel(string vessel_id)
        {
            return new RandomGenericRequests().Request(RandomData.RandomVessel, canBeNotFound: true, canBadRequest: false);
        }

        public Tuple<Vessel, ErrorResponse> UpdateVessel(string vessel_id, VesselRequest vessel)
        {
            return new RandomGenericRequests().Request(vessel.Create(vessel_id), canBeNotFound: true);
        }
    }
}
