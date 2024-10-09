using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomPackagesRequests : IPackagesRequests
    {
        public Tuple<List<IPackage>, ErrorResponse> GetPackages() => 
            new RandomGenericRequests().Request(RandomData.Packages, canBadRequest: false);


        public Tuple<IPackage, ErrorResponse> GetPackage(string pack_id)
        {
            IPackage res = RandomData.Package;
            res.Id = pack_id;
            return new RandomGenericRequests().Request(res, canBadRequest: false, canBeNotFound: true);
        }

        public Tuple<IPackage, ErrorResponse> UpdatePackage(string pack_id, UpdatePackage pack)
            => new RandomGenericRequests().Request(RandomData.Package, canBadRequest: false, canBeNotFound: true);
    }
}
