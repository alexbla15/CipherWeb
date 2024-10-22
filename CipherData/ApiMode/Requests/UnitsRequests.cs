namespace CipherData.ApiMode
{
    public class UnitsRequests : IUnitsRequests
    {
        private static readonly string path = "/units";

        public async Task<Tuple<List<IUnit>, ErrorResponse>> GetAll()
            => await GeneralAPIRequest.GetAll<IUnit, Unit>(path);

        public async Task<Tuple<IUnit, ErrorResponse>> GetById(string? id)
        {
            var result = await GeneralAPIRequest.Get<Unit>($"{path}/{id}");

            IUnit obj = result.Item1 ?? new Unit();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IUnit, ErrorResponse>> Create(IUnitRequest req)
        {
            var result = await GeneralAPIRequest.Post<Unit>(path, req);

            IUnit obj = result.Item1 ?? new Unit();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IUnit, ErrorResponse>> Update(string? id, IUnitRequest req)
        {
            var result = await GeneralAPIRequest.Put<Unit>($"{path}/{id}", req);

            IUnit obj = result.Item1 ?? new Unit();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
