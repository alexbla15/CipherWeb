namespace CipherData.ApiMode
{
    public class PackagesRequests : IPackagesRequests
    {
        private static readonly string path = "/packages";

        public async Task<Tuple<List<IPackage>, ErrorResponse>> GetPackages()
            => await GeneralAPIRequest.GetAll<IPackage, Package>(path);

        public async Task<Tuple<IPackage, ErrorResponse>> GetPackage(string id)
        {
            var result = await GeneralAPIRequest.Get<Package>($"{path}/{id}");

            IPackage obj = result.Item1 ?? new Package();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IPackage, ErrorResponse>> UpdatePackage(string pack_id, IUpdatePackage pack)
        {
            var result = await GeneralAPIRequest.Put<Package>($"{path}/{pack_id}", pack);

            IPackage obj = result.Item1 ?? new Package();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
