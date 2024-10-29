namespace CipherData.RandomMode
{
    public class RandomSystemsRequests : ISystemsRequests
    {
        public async Task<Tuple<IStorageSystem, ErrorResponse>> Create(ISystemRequest sys)
            => await new RandomGenericRequests().Request(sys.Create(RandomStorageSystem.GetNextId()));

        public async Task<Tuple<List<IStorageSystem>, ErrorResponse>> GetAll()
            => await new RandomGenericRequests().Request(RandomData.Systems, canBadRequest: false);

        public async Task<Tuple<IStorageSystem, ErrorResponse>> GetById(string? sys_id)
            => await new RandomGenericRequests().Request(new RandomStorageSystem() {Id=sys_id } as IStorageSystem, canBadRequest: false, canBeNotFound: true);

        public async Task<Tuple<IStorageSystem, ErrorResponse>> Update(string? sys_id, ISystemRequest sys)
            => await new RandomGenericRequests().Request(sys.Create(sys_id), canBeNotFound: true);

        public async Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> GetSystemConditions(string? id)
            => await new RandomGenericRequests().Request(RandomData.CustomObjectBooleanCondition, canBadRequest: false);

        public async Task<Tuple<ICustomObjectBooleanCondition, ErrorResponse>> UpdateSystemConditions(string? id, ICustomObjectBooleanCondition condition)
            => await new RandomGenericRequests().Request(RandomData.CustomObjectBooleanCondition, canBeNotFound: true);
    }
}
