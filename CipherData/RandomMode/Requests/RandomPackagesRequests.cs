namespace CipherData.RandomMode
{
    public class RandomPackagesRequests : IPackagesRequests
    {
        public async Task<Tuple<List<IPackage>, ErrorResponse>> GetAll() =>
            await new RandomGenericRequests().Request(RandomData.GetRandomPackages(new Random().Next(20)), canBadRequest: false);

        public Task<Tuple<IPackage, ErrorResponse>> Create(IUpdatePackage request)
            => throw new NotImplementedException("Cannot create package using this end point, use events");

        public async Task<Tuple<IPackage, ErrorResponse>> GetById(string? pack_id)
        {
            if (string.IsNullOrEmpty(pack_id)) 
                return Tuple.Create(new Package() as IPackage, ErrorResponse.BadRequest);
            IPackage res = new RandomPackage() { Id=pack_id};
            return await new RandomGenericRequests().Request(res, canBadRequest: false, canBeNotFound: true);
        }

        public async Task<Tuple<IPackage, ErrorResponse>> Update(string? pack_id, IUpdatePackage pack)
            => await GetById(pack_id);
    }
}
