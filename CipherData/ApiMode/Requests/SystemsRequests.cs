namespace CipherData.ApiMode
{
    public class SystemsRequests : ISystemsRequests
    {
        private static readonly string path = "/systems";

        public async Task<Tuple<List<IStorageSystem>, ErrorResponse>> GetSystems()
            => await GeneralAPIRequest.GetAll<IStorageSystem, StorageSystem>(path);

        public async Task<Tuple<IStorageSystem, ErrorResponse>> GetSystem(string id)
        {
            var result = await GeneralAPIRequest.Get<StorageSystem>($"{path}/{id}");

            IStorageSystem obj = result.Item1 ?? new StorageSystem();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IStorageSystem, ErrorResponse>> CreateSystem(ISystemRequest req)
        {
            var result = await GeneralAPIRequest.Post<StorageSystem>(path, req);

            IStorageSystem obj = result.Item1 ?? new StorageSystem();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IStorageSystem, ErrorResponse>> UpdateSystem(string id, ISystemRequest req)
        {
            var result = await GeneralAPIRequest.Put<StorageSystem>($"{path}/{id}", req);

            IStorageSystem obj = result.Item1 ?? new StorageSystem();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> GetSystemConditions(string id)
        {
            var result = await GeneralAPIRequest.Get<CustomObjectBooleanCondition>($"{path}/{id}/conditions");

            ICustomObjectBooleanCondition objs = result.Item1 ?? new CustomObjectBooleanCondition();
            return Tuple.Create(objs, result.Item2);
        }

        public async Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> UpdateSystemConditions(string id, ICustomObjectBooleanCondition cond)
        {
            var result = await GeneralAPIRequest.Put<CustomObjectBooleanCondition>($"{path}/{id}/conditions", cond);

            ICustomObjectBooleanCondition obj = result.Item1 ?? new CustomObjectBooleanCondition();
            return Tuple.Create(obj, result.Item2);
        }


    }
}
