namespace CipherData.ApiMode
{
    public class PackagesRequests : IPackagesRequests
    {
        private static readonly string path = "/packages";

        public Task<Tuple<IPackage, ErrorResponse>> Create(IUpdatePackage request)
            => throw new NotImplementedException("Cannot create package using this end point, use events");

        public async Task<Tuple<List<IPackage>, ErrorResponse>> GetAll()
            => await GeneralAPIRequest.GetAll<IPackage, Package>(path);

        public async Task<Tuple<IPackage, ErrorResponse>> GetById(string? id)
        {
            var result = await GeneralAPIRequest.Get<Package>($"{path}/{id}");

            IPackage obj = result.Item1 ?? new Package();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IPackage, ErrorResponse>> Update(string? pack_id, IUpdatePackage pack)
        {
            var result = await GeneralAPIRequest.Put<Package>($"{path}/{pack_id}", pack);

            IPackage obj = result.Item1 ?? new Package();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
