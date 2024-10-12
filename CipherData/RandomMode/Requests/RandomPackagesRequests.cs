namespace CipherData.RandomMode
{
    public class RandomPackagesRequests : IPackagesRequests
    {
        public async Task<Tuple<List<IPackage>, ErrorResponse>> GetPackages() =>
            await new RandomGenericRequests().Request(RandomData.Packages, canBadRequest: false);


        public async Task<Tuple<IPackage, ErrorResponse>> GetPackage(string pack_id)
        {
            IPackage res = RandomData.Package;
            res.Id = pack_id;
            return await new RandomGenericRequests().Request(res, canBadRequest: false, canBeNotFound: true);
        }

        public async Task<Tuple<IPackage, ErrorResponse>> UpdatePackage(string pack_id, IUpdatePackage pack)
            => await new RandomGenericRequests().Request(RandomData.Package, canBadRequest: false, canBeNotFound: true);
    }
}
