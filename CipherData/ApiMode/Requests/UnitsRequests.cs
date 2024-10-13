namespace CipherData.ApiMode
{
    public class UnitsRequests : IUnitsRequests
    {
        private static readonly string path = "/units";

        public async Task<Tuple<List<IUnit>, ErrorResponse>> GetUnits()
            => await GeneralAPIRequest.GetAll<IUnit, Unit>(path);

        public async Task<Tuple<IUnit, ErrorResponse>> GetUnit(string id)
        {
            var result = await GeneralAPIRequest.Get<Unit>($"{path}/{id}");

            IUnit obj = result.Item1 ?? new Unit();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IUnit, ErrorResponse>> CreateUnit(IUnitRequest req)
        {
            var result = await GeneralAPIRequest.Post<Unit>(path, req);

            IUnit obj = result.Item1 ?? new Unit();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IUnit, ErrorResponse>> UpdateUnit(string id, IUnitRequest req)
        {
            var result = await GeneralAPIRequest.Put<Unit>($"{path}/{id}", req);

            IUnit obj = result.Item1 ?? new Unit();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
