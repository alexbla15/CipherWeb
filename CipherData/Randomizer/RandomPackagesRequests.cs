using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomPackagesRequests : IPackagesRequests
    {
        public Tuple<List<Package>, ErrorResponse> GetPackages()
        {
            return new RandomGenericRequests().Request(RandomData.RandomPackages, canBadRequest: false);
        }

        public Tuple<Package, ErrorResponse> GetPackage(string pack_id)
        {
            Package res = RandomData.RandomPackage;
            res.Id = pack_id;
            return new RandomGenericRequests().Request(res, canBadRequest: false, canBeNotFound: true);
        }

        public Tuple<Package, ErrorResponse> UpdatePackage(string pack_id, UpdatePackage pack)
        {
            return new RandomGenericRequests().Request(RandomData.RandomPackage, canBadRequest: false, canBeNotFound: true);
        }
    }
}
