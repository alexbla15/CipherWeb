using CipherData.Models;
using CipherData.Models.Randomizers;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomSystemsRequests : ISystemsRequests
    {
        public Tuple<IStorageSystem, ErrorResponse> CreateSystem(ISystemRequest sys)
            => new RandomGenericRequests().Request(sys.Create(RandomStorageSystem.GetNextId()));

        public Tuple<List<IStorageSystem>, ErrorResponse> GetSystems()
            => new RandomGenericRequests().Request(RandomData.Systems, canBadRequest: false);

        public Tuple<IStorageSystem, ErrorResponse> GetSystem(string sys_id)
            => new RandomGenericRequests().Request(RandomData.System, canBadRequest: false, canBeNotFound: true);

        public Tuple<IStorageSystem, ErrorResponse> UpdateSystem(string sys_id, ISystemRequest sys)
            => new RandomGenericRequests().Request(sys.Create(sys_id), canBeNotFound: true);

        public Tuple<IGroupedBooleanCondition, ErrorResponse> GetSystemConditions()
            => new RandomGenericRequests().Request(RandomData.GroupedBooleanCondition, canBadRequest: false);

        public Tuple<ICustomObjectBooleanCondition, ErrorResponse> UpdateSystemConditions(ICustomObjectBooleanCondition condition)
            => new RandomGenericRequests().Request(RandomData.CustomObjectBooleanCondition, canBeNotFound: true);
    }
}
