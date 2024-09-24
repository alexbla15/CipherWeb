using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomSystemsRequests : ISystemsRequests
    {
        public Tuple<StorageSystem, ErrorResponse> CreateSystem(SystemRequest sys)
        {
            return new RandomGenericRequests().Request(sys.Create(StorageSystem.GetNextId()));
        }

        public Tuple<List<StorageSystem>, ErrorResponse> GetSystems()
        {
            return new RandomGenericRequests().Request(RandomData.RandomSystems, canBadRequest: false);
        }

        public Tuple<StorageSystem, ErrorResponse> GetSystem(string sys_id)
        {

            return new RandomGenericRequests().Request(RandomData.RandomSystem, canBadRequest: false, canBeNotFound: true);
        }

        public Tuple<StorageSystem, ErrorResponse> UpdateSystem(string sys_id, SystemRequest sys)
        {
            return new RandomGenericRequests().Request(sys.Create(sys_id), canBeNotFound: true);
        }

        public Tuple<GroupedBooleanCondition, ErrorResponse> GetSystemConditions()
        {
            return new RandomGenericRequests().Request(RandomData.RandomGroupedBooleanCondition, canBadRequest: false);
        }

        public Tuple<CustomObjectBooleanCondition, ErrorResponse> UpdateSystemConditions(CustomObjectBooleanCondition condition)
        {
            return new RandomGenericRequests().Request(RandomData.RandomCustomObjectBooleanCondition, canBeNotFound: true);
        }
    }
}
