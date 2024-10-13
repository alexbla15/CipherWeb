namespace CipherData.ApiMode
{
    public class VesselsRequests : IVesselsRequests
    {
        private static readonly string path = "/vessels";

        public async Task<Tuple<List<IVessel>, ErrorResponse>> GetVessels()
            => await GeneralAPIRequest.GetAll<IVessel, Vessel>(path);

        public async Task<Tuple<IVessel, ErrorResponse>> GetVessel(string? id)
        {
            if (string.IsNullOrEmpty(id)) return Tuple.Create(new Vessel() as IVessel, ErrorResponse.BadRequest);

            var result = await GeneralAPIRequest.Get<Vessel>($"{path}/{id}");

            IVessel obj = result.Item1 ?? new Vessel();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IVessel, ErrorResponse>> CreateVessel(IVesselRequest req)
        {
            var result = await GeneralAPIRequest.Post<Vessel>(path, req);

            IVessel obj = result.Item1 ?? new Vessel();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IVessel, ErrorResponse>> UpdateVessel(string? id, IVesselRequest req)
        {
            if (string.IsNullOrEmpty(id)) return Tuple.Create(new Vessel() as IVessel, ErrorResponse.BadRequest);

            var result = await GeneralAPIRequest.Put<Vessel>($"{path}/{id}", req);

            IVessel obj = result.Item1 ?? new Vessel();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
